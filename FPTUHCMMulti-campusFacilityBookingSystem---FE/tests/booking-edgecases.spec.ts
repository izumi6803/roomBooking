import { test, expect } from '@playwright/test';
import { login, getTomorrowDate, waitForToast, TEST_ACCOUNTS } from './helpers/auth';

type CreatedBooking = {
  facilityId: string;
  bookingDate: string;
  slotStart: string;
  purposeMarker: string;
};

const createBooking = async (page: import('@playwright/test').Page): Promise<CreatedBooking> => {
  const purposeMarker = `PW-EDGE-${Date.now()}`;

  await page.goto('/facilities');
  await page.waitForLoadState('networkidle');
  await page.waitForTimeout(1500);

  // Select campus if present
  const campusCard = page.locator('button:has-text("HCM"), button:has-text("FPT HCM")').first();
  if (await campusCard.isVisible().catch(() => false)) {
    await campusCard.click();
    await page.waitForTimeout(1000);
  }

  // Open first booking page
  await page.waitForSelector('a:has-text("Đặt ngay")', { timeout: 10000 });
  const bookNowBtn = page.locator('a:has-text("Đặt ngay")').first();
  await expect(bookNowBtn).toBeVisible();
  await bookNowBtn.click();

  await page.waitForURL(/\/booking\//, { timeout: 10000 });
  await page.waitForLoadState('networkidle');

  // Capture facilityId from URL
  const url = new URL(page.url());
  const parts = url.pathname.split('/');
  const facilityId = parts[parts.length - 1];

  // Pick tomorrow
  const bookingDate = getTomorrowDate();
  const dateInput = page.locator('input[type="date"]').first();
  await expect(dateInput).toBeVisible();
  await dateInput.fill(bookingDate);
  await page.waitForTimeout(1500);

  // Pick first available slot
  const timeSlotSection = page.locator('label', { hasText: 'Chọn khung giờ' }).locator('..');
  const slotButtons = timeSlotSection.locator('div.grid').first().locator('button');

  const slotCount = await slotButtons.count();
  expect(slotCount).toBeGreaterThan(0);

  let slotStart = '';
  for (let i = 0; i < slotCount; i++) {
    const btn = slotButtons.nth(i);
    if (await btn.isDisabled()) continue;
    const text = (await btn.textContent())?.trim() || '';
    if (!text) continue;
    await btn.click();
    slotStart = text;
    break;
  }

  if (!slotStart) {
    throw new Error('No available time slot found to create booking');
  }

  // Fill form
  const purposeInput = page.locator('input[type="text"]').filter({ hasNotText: '' }).nth(0);
  await expect(purposeInput).toBeVisible();
  await purposeInput.fill(`Edge-case booking ${purposeMarker}`);

  const attendeesInput = page.locator('input[type="number"]').first();
  if (await attendeesInput.isVisible().catch(() => false)) {
    await attendeesInput.fill('5');
  }

  const notesTextarea = page.locator('textarea').first();
  if (await notesTextarea.isVisible().catch(() => false)) {
    await notesTextarea.fill(`Notes ${purposeMarker}`);
  }

  // Submit
  const submitBtn = page.locator('button:has-text("Xác nhận đặt phòng")');
  await expect(submitBtn).toBeEnabled({ timeout: 10000 });
  await submitBtn.click();

  // Phone modal (optional)
  const phoneModal = page.locator('.fixed.inset-0').filter({ hasText: /Cập nhật số điện thoại|Số điện thoại/i }).first();
  if (await phoneModal.isVisible().catch(() => false)) {
    const phoneInput = phoneModal.locator('input[type="tel"], input[type="text"]').first();
    await phoneInput.fill('0987654321');
    const updateBtn = phoneModal.locator('button:has-text("Cập nhật"), button:has-text("Lưu")').first();
    await updateBtn.click();
    await page.waitForTimeout(800);
  }

  // Confirm booking modal
  const confirmModal = page.locator('.fixed.inset-0').filter({ hasText: 'Xác nhận đặt phòng' }).first();
  if (await confirmModal.isVisible().catch(() => false)) {
    const confirmBtn = confirmModal.locator('button').filter({ hasText: 'Xác nhận' }).last();
    await expect(confirmBtn).toBeVisible();
    await confirmBtn.click();
  }

  // Wait a bit for backend to persist
  await page.waitForTimeout(2000);

  return { facilityId, bookingDate, slotStart, purposeMarker };
};

const findBookingCardByPurpose = async (page: import('@playwright/test').Page, purposeMarker: string) => {
  const card = page.locator('.bg-white.rounded-xl').filter({ hasText: purposeMarker }).first();
  await expect(card).toBeVisible({ timeout: 15000 });
  return card;
};

test.describe('Booking Edge Cases', () => {
  test.describe.configure({ mode: 'serial' });

  test('1) Trùng lịch: slot bị disable khi đã có booking', async ({ page }) => {
    await login(page, TEST_ACCOUNTS.student.email, TEST_ACCOUNTS.student.password);

    const created = await createBooking(page);

    // Re-open same facility booking page, same date
    await page.goto(`/booking/${created.facilityId}`);
    await page.waitForLoadState('networkidle');

    const dateInput = page.locator('input[type="date"]').first();
    await expect(dateInput).toBeVisible();
    await dateInput.fill(created.bookingDate);
    await page.waitForTimeout(1500);

    const timeSlotSection = page.locator('label', { hasText: 'Chọn khung giờ' }).locator('..');
    const slotBtn = timeSlotSection.locator('button', { hasText: created.slotStart }).first();

    await expect(slotBtn).toBeVisible({ timeout: 10000 });
    await expect(slotBtn).toBeDisabled({ timeout: 10000 });
  });

  test('2) Hủy booking: chuyển trạng thái Đã hủy', async ({ page }) => {
    await login(page, TEST_ACCOUNTS.student.email, TEST_ACCOUNTS.student.password);

    const created = await createBooking(page);

    await page.goto('/my-bookings');
    await page.waitForLoadState('networkidle');

    const card = await findBookingCardByPurpose(page, created.purposeMarker);

    const cancelBtn = card.locator('button:has-text("Hủy đặt")').first();
    await expect(cancelBtn).toBeVisible();
    await cancelBtn.click();

    const modal = page.locator('.fixed.inset-0').filter({ hasText: 'Xác nhận hủy đặt phòng' }).first();
    await expect(modal).toBeVisible({ timeout: 5000 });

    const confirmCancel = modal.locator('button:has-text("Hủy đặt phòng")').first();
    await expect(confirmCancel).toBeVisible();
    await confirmCancel.click();

    // Verify toast or status label
    await waitForToast(page, /Đã hủy đặt phòng thành công/i);

    await page.waitForTimeout(1500);
    await page.reload();
    await page.waitForLoadState('networkidle');

    const cancelledCard = await findBookingCardByPurpose(page, created.purposeMarker);
    await expect(cancelledCard).toContainText(/Đã hủy|Cancelled/i);

    // Optional: slot becomes available again
    await page.goto(`/booking/${created.facilityId}`);
    await page.waitForLoadState('networkidle');

    const dateInput = page.locator('input[type="date"]').first();
    await dateInput.fill(created.bookingDate);
    await page.waitForTimeout(1500);

    const timeSlotSection = page.locator('label', { hasText: 'Chọn khung giờ' }).locator('..');
    const slotBtn = timeSlotSection.locator('button', { hasText: created.slotStart }).first();
    await expect(slotBtn).toBeVisible({ timeout: 10000 });
    await expect(slotBtn).toBeEnabled({ timeout: 10000 });
  });

  test('3) Admin từ chối booking: user thấy lý do', async ({ page }) => {
    // Student creates booking
    await login(page, TEST_ACCOUNTS.student.email, TEST_ACCOUNTS.student.password);
    const created = await createBooking(page);

    // Admin rejects
    await login(page, TEST_ACCOUNTS.admin.email, TEST_ACCOUNTS.admin.password);
    await page.goto('/admin/dashboard');
    await page.waitForLoadState('networkidle');
    await page.waitForTimeout(1000);

    const statusSelect = page.locator('select').first();
    await expect(statusSelect).toBeVisible();
    await statusSelect.selectOption('Pending_Approval');
    await page.waitForTimeout(1000);

    const adminCard = page.locator('div.bg-white').filter({ hasText: created.purposeMarker }).first();
    await expect(adminCard).toBeVisible({ timeout: 15000 });

    const rejectBtn = adminCard.locator('button:has-text("Từ chối")').first();
    await expect(rejectBtn).toBeVisible();
    await rejectBtn.click();

    const rejectModal = page.locator('.fixed.inset-0').filter({ hasText: 'Từ chối đặt phòng' }).first();
    await expect(rejectModal).toBeVisible({ timeout: 5000 });

    const reason = `Từ chối test ${Date.now()}`;
    await rejectModal.locator('textarea').fill(reason);

    const confirmReject = rejectModal.locator('button:has-text("Xác nhận từ chối")').first();
    await expect(confirmReject).toBeEnabled();
    await confirmReject.click();

    await page.waitForTimeout(2000);

    // Student verifies rejected + reason
    await login(page, TEST_ACCOUNTS.student.email, TEST_ACCOUNTS.student.password);
    await page.goto('/my-bookings');
    await page.waitForLoadState('networkidle');

    const card = await findBookingCardByPurpose(page, created.purposeMarker);
    await expect(card).toContainText(/Từ chối|Rejected/i);
    await expect(card).toContainText(new RegExp(reason));
  });

  test('4) Báo cáo (Admin Reports): tải trang và đổi bộ lọc', async ({ page }) => {
    await login(page, TEST_ACCOUNTS.admin.email, TEST_ACCOUNTS.admin.password);

    await page.goto('/admin/reports');
    await page.waitForLoadState('networkidle');

    await expect(page.locator('h1', { hasText: 'Báo cáo thống kê' })).toBeVisible({ timeout: 10000 });
    await expect(page.locator('text=Tổng số lượt đặt').first()).toBeVisible({ timeout: 10000 });

    // Change period type -> day
    const periodTypeSelect = page.locator('select').first();
    await periodTypeSelect.selectOption('day');
    await page.waitForTimeout(500);

    // Click refresh
    const refreshBtn = page.locator('button:has-text("Tải lại")').first();
    await expect(refreshBtn).toBeVisible();
    await refreshBtn.click();

    // Should still render overview cards
    await expect(page.locator('text=Tổng số lượt đặt').first()).toBeVisible({ timeout: 15000 });
  });
});

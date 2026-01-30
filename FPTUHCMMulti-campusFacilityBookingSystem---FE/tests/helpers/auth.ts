import { expect, Page } from '@playwright/test';

/**
 * Test Accounts - CẬP NHẬT THEO DATABASE THỰC TẾ CỦA BẠN
 * 
 * Yêu cầu:
 * - Student: Cần có phone, email, studentId (mssv) trong profile
 * - Admin: Tài khoản admin có quyền duyệt booking
 * - Lecturer: Tài khoản giảng viên (optional)
 */
export const TEST_ACCOUNTS = {
  student: {
    email: 'studentA',           // Username đăng nhập
    password: 'password123',     // Password
    // Thông tin profile (cần đã được setup trong DB)
    expectedPhone: '0987654321',
    expectedStudentId: 'SE12345'
  },
  admin: {
    email: 'admin',
    password: 'admin123'
  },
  lecturer: {
    email: 'lecturerB',
    password: 'password123'
  }
};

// Biến chia sẻ giữa các test
export const sharedState = {
  bookingId: '',
  facilityId: '',
  bookingDate: ''
};

/**
 * Login helper function
 */
export async function login(page: Page, email: string, password: string): Promise<void> {
  // Fail fast (do not allow tests to continue while still on /login)
  await page.context().clearCookies();

  // Ensure we're on the same origin before clearing storage.
  await page.goto('/', { waitUntil: 'domcontentloaded' });
  await page.evaluate(() => {
    sessionStorage.clear();
    localStorage.clear();
  });

  await page.goto('/login', { waitUntil: 'domcontentloaded' });

  // Wait for login page
  await page.getByRole('heading', { name: /đăng nhập/i }).waitFor({ timeout: 15000 });

  // Some builds have multiple login modes; ensure "Tài khoản được cấp" is selected when present.
  const providedAccountTab = page.getByRole('button', { name: /tài khoản được cấp/i });
  if (await providedAccountTab.isVisible().catch(() => false)) {
    await providedAccountTab.click();
  }

  // Use accessible selectors instead of input[type=password] (password may be rendered as type=text).
  const usernameInput = page.getByRole('textbox', { name: /tên đăng nhập/i });
  const passwordInput = page.getByRole('textbox', { name: /^password$/i });

  await expect(usernameInput).toBeVisible({ timeout: 15000 });
  await expect(passwordInput).toBeVisible({ timeout: 15000 });

  await usernameInput.fill(email);
  await passwordInput.fill(password);

  await page.getByRole('button', { name: /đăng nhập/i }).click();

  try {
    await page.waitForURL(url => !url.pathname.includes('/login'), { timeout: 15000 });
  } catch {
    // Common failure mode: still on /login due to wrong mode/creds/backend.
    throw new Error('Login failed: still on /login after submit');
  }
}

/**
 * Logout helper function
 */
export async function logout(page: Page): Promise<void> {
  // Clear session
  await page.evaluate(() => {
    sessionStorage.removeItem('auth_token');
    sessionStorage.removeItem('auth_user');
    localStorage.clear();
  });
  
  // Navigate to login
  await page.goto('/login');
}

/**
 * Get tomorrow's date in YYYY-MM-DD format
 */
export function getTomorrowDate(): string {
  const tomorrow = new Date();
  tomorrow.setDate(tomorrow.getDate() + 1);
  return tomorrow.toISOString().split('T')[0];
}

/**
 * Get date X days from now in YYYY-MM-DD format
 */
export function getDateFromNow(days: number): string {
  const date = new Date();
  date.setDate(date.getDate() + days);
  return date.toISOString().split('T')[0];
}

/**
 * Format date for display
 */
export function formatDateVN(dateStr: string): string {
  const date = new Date(dateStr);
  return date.toLocaleDateString('vi-VN', {
    day: '2-digit',
    month: '2-digit',
    year: 'numeric'
  });
}

/**
 * Wait for toast notification
 */
export async function waitForToast(page: Page, textPattern: RegExp | string): Promise<boolean> {
  try {
    const toast = page.locator('.toast, [role="alert"], .notification');
    await toast.filter({ hasText: textPattern }).waitFor({ timeout: 10000 });
    return true;
  } catch {
    return false;
  }
}

/**
 * Take screenshot with timestamp
 */
export async function takeScreenshot(page: Page, name: string): Promise<void> {
  const timestamp = new Date().toISOString().replace(/[:.]/g, '-');
  await page.screenshot({ 
    path: `./test-results/screenshots/${name}-${timestamp}.png`,
    fullPage: true 
  });
}

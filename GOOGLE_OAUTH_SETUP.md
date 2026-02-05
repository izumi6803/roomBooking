# How to Get Google OAuth Credentials

This guide will help you obtain Google Client ID and Client Secret for OAuth authentication.

## Step 1: Create a Google Cloud Project

1. Go to [Google Cloud Console](https://console.cloud.google.com/)
2. Sign in with your Google account
3. Click on the project dropdown at the top of the page
4. Click **"NEW PROJECT"**
5. Enter a project name (e.g., "FPTU Facility Booking")
6. Click **"CREATE"**
7. Wait for the project to be created, then select it

## Step 2: Enable Google+ API

1. In the Google Cloud Console, go to **"APIs & Services"** → **"Library"**
2. Search for **"Google+ API"** or **"Google Identity"**
3. Click on **"Google+ API"**
4. Click **"ENABLE"**

## Step 3: Configure OAuth Consent Screen

1. Go to **"APIs & Services"** → **"OAuth consent screen"**
2. Choose **"External"** user type (unless you have a Google Workspace)
3. Click **"CREATE"**

4. Fill in the required information:
   - **App name**: FPTU Facility Booking System
   - **User support email**: Your email address
   - **App logo**: (Optional) Upload your app logo
   - **Application home page**: Your frontend URL
   - **Authorized domains**: 
     - Add `railway.app` (for Railway deployment)
     - Add your custom domain if you have one
   - **Developer contact email**: Your email address

5. Click **"SAVE AND CONTINUE"**

6. **Scopes** (Optional for now):
   - Click **"ADD OR REMOVE SCOPES"**
   - Select:
     - `../auth/userinfo.email`
     - `../auth/userinfo.profile`
     - `openid`
   - Click **"UPDATE"**
   - Click **"SAVE AND CONTINUE"**

7. **Test users** (For development):
   - Click **"ADD USERS"**
   - Add email addresses of users who can test the app
   - Click **"ADD"**
   - Click **"SAVE AND CONTINUE"**

8. Review your settings and click **"BACK TO DASHBOARD"**

## Step 4: Create OAuth Credentials

1. Go to **"APIs & Services"** → **"Credentials"**
2. Click **"CREATE CREDENTIALS"** → **"OAuth client ID"**
3. Select **"Web application"** as the application type
4. Fill in the details:

   **Name**: FPTU Booking System Web Client

   **Authorized JavaScript origins**:
   - For local development: `http://localhost:5173`
   - For Railway: `https://your-frontend-service.railway.app`
   - Add both if you want to test locally and in production

   **Authorized redirect URIs**:
   - For local development: `http://localhost:5173`
   - For Railway: `https://your-frontend-service.railway.app`
   - You can add multiple URIs

5. Click **"CREATE"**

## Step 5: Copy Your Credentials

A dialog will appear with your credentials:

```
Your Client ID
1234567890-abcdefghijklmnop.apps.googleusercontent.com

Your Client Secret
GOCSPX-abcdefghijklmnopqrstuvwxyz
```

**IMPORTANT**: 
- Copy both the **Client ID** and **Client Secret**
- Store them securely
- You can always view them again in the Credentials page

## Step 6: Update Your Application

### For Local Development

#### Frontend (.env)
```bash
VITE_API_BASE_URL=http://localhost:5252
VITE_GOOGLE_CLIENT_ID=YOUR_CLIENT_ID_HERE
```

#### Backend (.env)
```bash
GOOGLE_CLIENT_ID=YOUR_CLIENT_ID_HERE
GOOGLE_CLIENT_SECRET=YOUR_CLIENT_SECRET_HERE
```

### For Railway Deployment

#### Frontend Service
Go to your frontend service in Railway → Variables:
```
VITE_API_BASE_URL=https://your-backend-service.railway.app
VITE_GOOGLE_CLIENT_ID=YOUR_CLIENT_ID_HERE
```

#### Backend Service
Go to your backend service in Railway → Variables:
```
GOOGLE_CLIENT_ID=YOUR_CLIENT_ID_HERE
GOOGLE_CLIENT_SECRET=YOUR_CLIENT_SECRET_HERE
```

## Step 7: Update Authorized URIs After Deployment

After deploying to Railway:

1. Go back to **Google Cloud Console** → **APIs & Services** → **Credentials**
2. Click on your OAuth client ID
3. Under **Authorized JavaScript origins**, add:
   - `https://your-actual-frontend-url.railway.app`
4. Under **Authorized redirect URIs**, add:
   - `https://your-actual-frontend-url.railway.app`
5. Click **"SAVE"**

## Important Notes

### Security
- **NEVER** commit `.env` files to Git
- Keep your Client Secret private
- Only share credentials through secure channels

### OAuth Consent Screen Status
- **Testing**: Only test users can sign in (up to 100 users)
- **In Production**: Anyone with a Google account can sign in
- To publish your app, you need to submit for verification

### Publishing Your App (Optional)

If you want anyone to use your app:
1. Go to **OAuth consent screen**
2. Click **"PUBLISH APP"**
3. For apps requesting sensitive scopes, you may need to go through Google's verification process

### Common Issues

**"Error 400: redirect_uri_mismatch"**
- Make sure the redirect URI in your code matches exactly with Google Console
- Check for trailing slashes
- Ensure protocol matches (http vs https)

**"This app isn't verified"**
- Normal for apps in testing mode
- Users will see a warning but can proceed by clicking "Advanced" → "Go to [App name]"
- To remove this, publish your app (may require verification)

## Testing

1. Start your local development server
2. Try logging in with Google
3. If successful, you should see the Google login popup
4. After authentication, you should be redirected back to your app

## Quick Reference

**Where to find credentials after creation:**
1. Go to [Google Cloud Console](https://console.cloud.google.com/)
2. Select your project
3. **APIs & Services** → **Credentials**
4. Click on your OAuth 2.0 Client ID name
5. View your Client ID and Client Secret

## Support

If you encounter issues:
- Check [Google Identity Documentation](https://developers.google.com/identity/protocols/oauth2)
- Verify all URLs match exactly (including http/https)
- Ensure the OAuth consent screen is configured
- Check that test users are added (for testing phase)

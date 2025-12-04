# Hattie AI Chatbot Embedding Guide

This guide explains how to easily embed the Hattie AI chatbot into your website. We support both standard HTML/React projects and Next.js applications.

## Prerequisites

1.  **Chatbot URL**: You need the URL where the chatbot script is hosted (e.g., `https://hattie.touchpointe.digital`).
2.  **Tenant ID**: Your unique identifier (e.g., `569d7a6f-b921-411b-895a-b9aa75ef5562`).

---

## Option 1: Standard HTML / React (SPA)

For standard websites or React Single Page Applications (SPAs), add the code to your `index.html` file.

### Instructions

1.  Open your `public/index.html` (or just `index.html`).
2.  Paste the following code just before the closing `</body>` tag.

```html
<!-- Hattie AI Configuration -->
<script>
  window.HattieAI = {
    tenantId: "YOUR_TENANT_ID_HERE", // Replace with your actual Tenant ID
    apiUrl: "https://hattie.touchpointe.digital" // Optional: API URL
  };
</script>

<!-- Load Chatbot -->
<link rel="stylesheet" href="https://hattie.touchpointe.digital/assets/index.css">
<script type="module" src="https://hattie.touchpointe.digital/assets/index.js"></script>
```

---

## Option 2: Next.js

For Next.js applications (App Router), use the `Script` component in your Root Layout.

### Instructions

1.  Open `app/layout.tsx` (or `app/layout.js`).
2.  Import the `Script` component from `next/script`.
3.  Add the configuration and script loading logic.

```tsx
import Script from 'next/script';
import './globals.css'; // Your global styles

export default function RootLayout({ children }: { children: React.ReactNode }) {
  return (
    <html lang="en">
      <head>
        {/* Load Chatbot CSS */}
        <link rel="stylesheet" href="https://hattie.touchpointe.digital/assets/index.css" />
      </head>
      <body>
        {children}

        {/* Hattie AI Configuration & Script */}
        <Script id="hattie-ai-config" strategy="beforeInteractive">
          {`
            window.HattieAI = {
              tenantId: "YOUR_TENANT_ID_HERE", // Replace with your actual Tenant ID
              apiUrl: "https://hattie.touchpointe.digital"
            };
          `}
        </Script>
        <Script 
          src="https://hattie.touchpointe.digital/assets/index.js" 
          type="module" 
          strategy="afterInteractive" 
        />
      </body>
    </html>
  );
}
```

### Note for Next.js Pages Router
If you are using the older Pages Router (`pages/_document.js`), you can add the `<script>` and `<link>` tags directly in the `<Head>` and `<body>` components similar to the HTML example.

---

## Configuration Options

| Option | Type | Description | Required |
| :--- | :--- | :--- | :--- |
| `tenantId` | `string` | Your unique chatbot identifier. | **Yes** |
| `apiUrl` | `string` | The backend API URL. Default: `https://hattie.touchpointe.digital` | No |

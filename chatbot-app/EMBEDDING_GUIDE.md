# Hattie AI Chatbot Embedding Guide

This guide explains how to embed the Hattie AI chatbot widget into any third-party website.

## Prerequisites

1.  **Host the Chatbot Files**: The chatbot application (built React app) must be hosted on a publicly accessible URL (e.g., `https://chat.hattie.ai` or a CDN).
2.  **Tenant ID**: You need the unique `Tenant ID` for the client you are embedding the bot for. This is obtained from the Hattie AI Admin Portal.

## Embedding Instructions

To add the chatbot to a website, simply add the following code snippet to the `<head>` or before the closing `</body>` tag of the website's HTML.

### 1. Configuration
First, define the configuration object. This tells the chatbot which tenant's data to load.

```html
<script>
  window.HattieAI = {
    tenantId: "YOUR_TENANT_ID_HERE" // Replace with the actual Tenant ID
  };
</script>
```

### 2. Load the Chatbot
Next, load the chatbot's JavaScript and CSS files. Replace `https://your-chatbot-domain.com` with the actual URL where you are hosting the chatbot app.

```html
<!-- Load Chatbot Styles -->
<link rel="stylesheet" href="https://your-chatbot-domain.com/assets/index.css">

<!-- Load Chatbot Script -->
<script type="module" src="https://your-chatbot-domain.com/assets/index.js"></script>
```

### Complete Example

Here is the full snippet combined:

```html
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Client Website</title>
    
    <!-- Hattie AI Chatbot Embed -->
    <script>
      window.HattieAI = {
        tenantId: "569d7a6f-b921-411b-895a-b9aa75ef5562" 
      };
    </script>
    <link rel="stylesheet" href="https://your-chatbot-domain.com/assets/index.css">
    <script type="module" src="https://your-chatbot-domain.com/assets/index.js"></script>
    <!-- End Hattie AI Chatbot Embed -->

</head>
<body>
    <h1>Welcome to our website</h1>
    <!-- The chatbot will appear automatically in the bottom right corner -->
</body>
</html>
```

## How it Works

*   The script automatically creates a container element (`<div id="hattie-ai-root">`) in the document body if it doesn't exist.
*   It reads the `tenantId` from the global `window.HattieAI` object.
*   It initializes the React application within that container, rendering the floating action button and chat interface.

## Troubleshooting

*   **Chatbot not appearing?** Check the browser console for errors. Ensure the `tenantId` is correct and the script URLs are reachable.
*   **Styling conflicts?** The chatbot uses scoped CSS and Shadow DOM techniques where possible, but if you see layout issues, ensure the `index.css` file is loaded correctly.
*   **CORS Issues?** Ensure your chatbot hosting server allows Cross-Origin Resource Sharing (CORS) from the client's domain.

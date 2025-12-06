# How to Add New Languages to Hattie AI

This guide explains how to add support for a new language in the Hattie AI system. This process involves two main steps: adding the language to the database (Backend) and adding translations to the chatbot interface (Frontend).

## Step 1: Backend - Add Language to Database

The list of supported languages is stored in the database. To add a new one, you need to create a database migration.

1.  **Open `HattieDbContext.cs`**
    *   Navigate to: `HattieAI.Infrastructure\Persistence\HattieDbContext.cs`
    *   Find the `OnModelCreating` method where languages are seeded.
    *   Add your new language to the list.
    
    ```csharp
    // Example: Adding French (fr)
    new Language { Code = "fr", Name = "French" },
    ```

2.  **Create a Migration**
    *   Open your terminal in the solution root folder.
    *   Run the following command to generate a migration file:
        ```powershell
        dotnet ef migrations add AddFrenchLanguage --project HattieAI.Infrastructure --startup-project HattieAI.API
        ```

3.  **Apply the Migration**
    *   Update the database with the new language:
        ```powershell
        dotnet ef database update --project HattieAI.Infrastructure --startup-project HattieAI.API
        ```

## Step 2: Frontend - Add Translations

The chatbot interface needs translations for the UI elements (Greeting, "Today", Placeholder, etc.) for the new language.

1.  **Open `ChatInterface.tsx`**
    *   Navigate to: `chatbot-app\src\components\chat\ChatInterface.tsx`

2.  **Update the `translations` object**
    *   Find the `translations` constant at the top of the file.
    *   Add a new key for your language code (e.g., `fr`) and provide the translated strings.

    ```typescript
    const translations: Record<string, any> = {
        // ... existing languages
        fr: { 
            greeting: 'Bonjour ! Je suis votre assistant IA.', 
            today: "Aujourd'hui", 
            placeholder: 'Message...', 
            privacy: 'Confidentialité' 
        },
    };
    ```

    > **Note:** Ensure the language code matches what you added to the database (e.g., `fr`). If you use a region code like `fr-CA`, the system will automatically fallback to `fr` if `fr-CA` is not explicitly defined, but it's best to match the base code.

## Step 3: Enable for Tenant (Optional)

If you want this language to be available for a specific client (Tenant), you need to enable it in the Admin Portal.

1.  Log in to the **Hattie AI Admin Portal**.
2.  Go to **Clients**.
3.  Edit the desired Client.
4.  In the **Supported Languages** section, select the new language you just added.
5.  Save changes.

---

**Verification:**
*   Refresh the Chatbot.
*   The new language should appear in the language selector (if enabled for that tenant).
*   Selecting it should update the UI text to your new translations.

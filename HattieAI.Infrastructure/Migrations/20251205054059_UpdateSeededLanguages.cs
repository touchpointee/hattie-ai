using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HattieAI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSeededLanguages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /*
            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "aa");
            
            // ... (all other DeleteData calls) ...
            
            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "zu-ZA");
                table: "Languages",
                keyColumn: "Code",
                keyValue: "en-BB");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "en-BE");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "en-BI");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "en-BM");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "en-BS");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "en-BW");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "en-BZ");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "en-CA");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "en-CC");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "en-CH");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "en-CK");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "en-CM");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "en-CX");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "en-CY");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "en-DE");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "en-DK");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "en-DM");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "en-ER");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "en-FI");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "en-FJ");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "en-FK");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "en-FM");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "en-GB");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "en-GD");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "en-GG");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "en-GH");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "en-GI");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "en-GM");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "en-GU");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "en-GY");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "en-HK");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "en-ID");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "en-IE");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "en-IL");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "en-IM");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "en-IN");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "en-IO");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "en-JE");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "en-JM");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "en-KE");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "en-KI");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "en-KN");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "en-KY");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "en-LC");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "en-LR");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "en-LS");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "en-MG");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "en-MH");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "en-MO");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "en-MP");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "en-MS");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "en-MT");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "en-MU");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "en-MV");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "en-MW");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "en-MY");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "en-NA");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "en-NF");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "en-NG");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "en-NL");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "en-NR");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "en-NU");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "en-NZ");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "en-PG");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "en-PH");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "en-PK");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "en-PN");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "en-PR");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "en-PW");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "en-RW");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "en-SB");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "en-SC");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "en-SD");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "en-SE");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "en-SG");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "en-SH");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "en-SI");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "en-SL");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "en-SS");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "en-SX");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "en-SZ");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "en-TC");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "en-TK");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "en-TO");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "en-TT");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "en-TV");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "en-TZ");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "en-UG");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "en-UM");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "en-US");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "en-US-POSIX");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "en-VC");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "en-VG");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "en-VI");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "en-VU");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "en-WS");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "en-ZA");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "en-ZM");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "en-ZW");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "eo");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "eo-001");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "es-419");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "es-AR");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "es-BO");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "es-BR");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "es-BZ");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "es-CL");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "es-CO");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "es-CR");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "es-CU");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "es-DO");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "es-EC");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "es-ES");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "es-GQ");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "es-GT");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "es-HN");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "es-MX");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "es-NI");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "es-PA");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "es-PE");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "es-PH");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "es-PR");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "es-PY");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "es-SV");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "es-US");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "es-UY");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "es-VE");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "et");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "et-EE");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "eu");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "eu-ES");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "ewo");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "ewo-CM");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "fa-AF");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "fa-IR");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "ff");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "ff-Adlm");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "ff-Adlm-BF");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "ff-Adlm-CM");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "ff-Adlm-GH");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "ff-Adlm-GM");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "ff-Adlm-GN");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "ff-Adlm-GW");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "ff-Adlm-LR");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "ff-Adlm-MR");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "ff-Adlm-NE");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "ff-Adlm-NG");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "ff-Adlm-SL");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "ff-Adlm-SN");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "ff-Latn");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "ff-Latn-BF");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "ff-Latn-CM");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "ff-Latn-GH");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "ff-Latn-GM");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "ff-Latn-GN");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "ff-Latn-GW");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "ff-Latn-LR");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "ff-Latn-MR");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "ff-Latn-NE");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "ff-Latn-NG");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "ff-Latn-SL");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "ff-Latn-SN");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "fi");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "fi-FI");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "fil");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "fil-PH");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "fo");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "fo-DK");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "fo-FO");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "fr-029");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "fr-BE");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "fr-BF");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "fr-BI");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "fr-BJ");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "fr-BL");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "fr-CA");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "fr-CD");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "fr-CF");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "fr-CG");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "fr-CH");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "fr-CI");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "fr-CM");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "fr-DJ");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "fr-DZ");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "fr-FR");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "fr-GA");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "fr-GF");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "fr-GN");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "fr-GP");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "fr-GQ");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "fr-HT");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "fr-KM");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "fr-LU");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "fr-MA");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "fr-MC");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "fr-MF");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "fr-MG");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "fr-ML");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "fr-MQ");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "fr-MR");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "fr-MU");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "fr-NC");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "fr-NE");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "fr-PF");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "fr-PM");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "fr-RE");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "fr-RW");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "fr-SC");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "fr-SN");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "fr-SY");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "fr-TD");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "fr-TG");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "fr-TN");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "fr-VU");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "fr-WF");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "fr-YT");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "fur");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "fur-IT");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "fy");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "fy-NL");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "ga");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "ga-GB");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "ga-IE");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "gd");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "gd-GB");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "gl");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "gl-ES");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "gn");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "gn-PY");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "gsw");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "gsw-CH");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "gsw-FR");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "gsw-LI");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "gu-IN");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "guz");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "guz-KE");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "gv");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "gv-IM");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "ha-GH");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "ha-NE");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "ha-NG");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "haw");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "haw-US");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "he");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "he-IL");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "hi-IN");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "hi-Latn");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "hi-Latn-IN");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "hr");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "hr-BA");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "hr-HR");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "hsb");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "hsb-DE");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "hu");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "hu-HU");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "hy");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "hy-AM");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "ia");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "ia-001");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "ibb");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "ibb-NG");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "id-ID");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "ig");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "ig-NG");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "ii");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "ii-CN");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "is");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "is-IS");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "it-CH");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "it-IT");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "it-SM");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "it-VA");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "iu");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "iu-CA");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "iu-Latn");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "iu-Latn-CA");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "ja-JP");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "jgo");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "jgo-CM");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "jmc");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "jmc-TZ");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "jv-ID");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "jv-Java");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "jv-Java-ID");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "ka");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "ka-GE");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "kab");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "kab-DZ");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "kam");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "kam-KE");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "kde");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "kde-TZ");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "kea");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "kea-CV");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "kgp");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "kgp-BR");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "khq");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "khq-ML");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "ki");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "ki-KE");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "kk");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "kk-KZ");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "kkj");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "kkj-CM");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "kl");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "kl-GL");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "kln");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "kln-KE");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "km-KH");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "kn-IN");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "ko-KP");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "ko-KR");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "kok");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "kok-IN");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "kr");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "kr-Latn");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "kr-Latn-NG");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "ks");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "ks-Arab");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "ks-Arab-IN");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "ks-Deva");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "ks-Deva-IN");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "ksb");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "ksb-TZ");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "ksf");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "ksf-CM");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "ksh");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "ksh-DE");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "kw");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "kw-GB");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "ky");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "ky-KG");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "la");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "la-VA");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "lag");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "lag-TZ");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "lb");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "lb-LU");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "lg");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "lg-UG");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "lkt");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "lkt-US");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "ln");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "ln-AO");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "ln-CD");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "ln-CF");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "ln-CG");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "lo");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "lo-LA");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "lrc");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "lrc-IQ");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "lrc-IR");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "lt");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "lt-LT");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "lu");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "lu-CD");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "luo");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "luo-KE");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "luy");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "luy-KE");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "lv");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "lv-LV");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "mai-IN");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "mas");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "mas-KE");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "mas-TZ");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "mer");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "mer-KE");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "mfe");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "mfe-MU");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "mg");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "mg-MG");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "mgh");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "mgh-MZ");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "mgo");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "mgo-CM");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "mi");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "mi-NZ");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "mk");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "mk-MK");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "ml-IN");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "mn");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "mn-MN");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "mn-Mong");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "mn-Mong-CN");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "mn-Mong-MN");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "mni");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "mni-Beng");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "mni-Beng-IN");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "moh");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "moh-CA");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "mr-IN");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "ms-BN");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "ms-ID");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "ms-MY");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "ms-SG");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "mt");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "mt-MT");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "mua");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "mua-CM");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "my-MM");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "mzn");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "mzn-IR");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "naq");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "naq-NA");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "nb");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "nb-NO");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "nb-SJ");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "nd");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "nd-ZW");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "nds");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "nds-DE");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "nds-NL");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "ne-IN");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "ne-NP");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "nl-AW");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "nl-BE");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "nl-BQ");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "nl-CW");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "nl-NL");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "nl-SR");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "nl-SX");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "nmg");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "nmg-CM");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "nn");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "nn-NO");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "nnh");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "nnh-CM");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "no");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "nqo");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "nqo-GN");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "nr");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "nr-ZA");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "nso");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "nso-ZA");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "nus");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "nus-SS");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "nyn");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "nyn-UG");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "oc");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "oc-ES");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "oc-FR");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "om");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "om-ET");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "om-KE");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "or-IN");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "os");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "os-GE");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "os-RU");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "pa-Arab");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "pa-Arab-PK");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "pa-Guru");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "pa-Guru-IN");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "pap");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "pap-029");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "pcm");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "pcm-NG");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "pl-PL");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "prg");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "prg-001");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "ps");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "ps-AF");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "ps-PK");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "pt-AO");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "pt-BR");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "pt-CH");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "pt-CV");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "pt-GQ");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "pt-GW");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "pt-LU");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "pt-MO");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "pt-MZ");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "pt-PT");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "pt-ST");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "pt-TL");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "qu");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "qu-BO");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "qu-EC");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "qu-PE");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "quc");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "quc-GT");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "raj");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "raj-IN");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "rm");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "rm-CH");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "rn");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "rn-BI");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "ro-MD");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "ro-RO");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "rof");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "rof-TZ");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "ru-BY");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "ru-KG");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "ru-KZ");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "ru-MD");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "ru-RU");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "ru-UA");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "rw");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "rw-RW");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "rwk");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "rwk-TZ");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "sa");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "sa-IN");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "sah");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "sah-RU");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "saq");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "saq-KE");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "sat");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "sat-Olck");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "sat-Olck-IN");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "sbp");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "sbp-TZ");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "sc");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "sc-IT");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "sd-Arab");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "sd-Arab-PK");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "sd-Deva");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "sd-Deva-IN");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "se");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "se-FI");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "se-NO");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "se-SE");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "seh");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "seh-MZ");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "ses");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "ses-ML");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "sg");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "sg-CF");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "shi");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "shi-Latn");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "shi-Latn-MA");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "shi-Tfng");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "shi-Tfng-MA");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "si-LK");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "sk");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "sk-SK");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "sl");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "sl-SI");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "sma");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "sma-NO");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "sma-SE");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "smj");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "smj-NO");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "smj-SE");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "smn");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "smn-FI");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "sms");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "sms-FI");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "sn");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "sn-ZW");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "so");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "so-DJ");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "so-ET");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "so-KE");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "so-SO");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "sq");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "sq-AL");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "sq-MK");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "sq-XK");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "sr");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "sr-Cyrl");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "sr-Cyrl-BA");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "sr-Cyrl-ME");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "sr-Cyrl-RS");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "sr-Cyrl-XK");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "sr-Latn");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "sr-Latn-BA");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "sr-Latn-ME");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "sr-Latn-RS");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "sr-Latn-XK");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "ss");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "ss-SZ");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "ss-ZA");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "ssy");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "ssy-ER");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "st");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "st-LS");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "st-ZA");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "su-Latn");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "su-Latn-ID");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "sv");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "sv-AX");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "sv-FI");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "sv-SE");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "sw");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "sw-CD");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "sw-KE");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "sw-TZ");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "sw-UG");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "syr");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "syr-SY");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "ta-IN");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "ta-LK");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "ta-MY");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "ta-SG");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "te-IN");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "teo");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "teo-KE");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "teo-UG");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "tg");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "tg-TJ");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "th-TH");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "ti");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "ti-ER");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "ti-ET");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "tig");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "tig-ER");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "tk");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "tk-TM");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "tn");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "tn-BW");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "tn-ZA");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "to");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "to-TO");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "tr-CY");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "tr-TR");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "ts");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "ts-ZA");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "tt");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "tt-RU");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "twq");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "twq-NE");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "tzm");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "tzm-Arab");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "tzm-Arab-MA");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "tzm-DZ");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "tzm-MA");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "tzm-Tfng");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "tzm-Tfng-MA");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "ug");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "ug-CN");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "uk-UA");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "ur-IN");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "ur-PK");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "uz-Arab");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "uz-Arab-AF");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "uz-Cyrl");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "uz-Cyrl-UZ");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "uz-Latn");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "uz-Latn-UZ");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "vai");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "vai-Latn");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "vai-Latn-LR");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "vai-Vaii");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "vai-Vaii-LR");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "ve");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "ve-ZA");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "vi-VN");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "vo");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "vo-001");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "vun");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "vun-TZ");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "wae");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "wae-CH");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "wal");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "wal-ET");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "wo");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "wo-SN");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "xh");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "xh-ZA");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "xog");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "xog-UG");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "yav");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "yav-CM");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "yi");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "yi-001");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "yo-BJ");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "yo-NG");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "yrl");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "yrl-BR");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "yrl-CO");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "yrl-VE");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "zgh");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "zgh-MA");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "zh");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "zh-Hans");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "zh-Hans-CN");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "zh-Hans-HK");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "zh-Hans-MO");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "zh-Hans-SG");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "zh-Hant");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "zh-Hant-HK");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "zh-Hant-MO");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "zh-Hant-TW");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "zu");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "zu-ZA");
            */

            migrationBuilder.UpdateData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "bn",
                column: "Name",
                value: "Bengali");

            migrationBuilder.UpdateData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "fa",
                column: "Name",
                value: "Persian (Farsi)");

            migrationBuilder.UpdateData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "or",
                column: "Name",
                value: "Odia (Oriya)");

            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "Code", "Name" },
                values: new object[,]
                {
                    { "skr", "Saraiki" },
                    { "yue", "Yue Chinese (Cantonese)" },
                    { "zh-CN", "Mandarin Chinese" },
                    { "zh-TW", "Standard Chinese (Traditional)" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "skr");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "yue");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "zh-CN");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "zh-TW");

            migrationBuilder.UpdateData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "bn",
                column: "Name",
                value: "Bangla");

            migrationBuilder.UpdateData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "fa",
                column: "Name",
                value: "Persian");

            migrationBuilder.UpdateData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "or",
                column: "Name",
                value: "Odia");

            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "Code", "Name" },
                values: new object[,]
                {
                    { "aa", "Afar" },
                    { "aa-DJ", "Afar (Djibouti)" },
                    { "aa-ER", "Afar (Eritrea)" },
                    { "aa-ET", "Afar (Ethiopia)" },
                    { "af", "Afrikaans" },
                    { "af-NA", "Afrikaans (Namibia)" },
                    { "af-ZA", "Afrikaans (South Africa)" },
                    { "agq", "Aghem" },
                    { "agq-CM", "Aghem (Cameroon)" },
                    { "ak", "Akan" },
                    { "ak-GH", "Akan (Ghana)" },
                    { "am-ET", "Amharic (Ethiopia)" },
                    { "ar-001", "Arabic (world)" },
                    { "ar-AE", "Arabic (United Arab Emirates)" },
                    { "ar-BH", "Arabic (Bahrain)" },
                    { "ar-DJ", "Arabic (Djibouti)" },
                    { "ar-DZ", "Arabic (Algeria)" },
                    { "ar-EG", "Arabic (Egypt)" },
                    { "ar-ER", "Arabic (Eritrea)" },
                    { "ar-IL", "Arabic (Israel)" },
                    { "ar-IQ", "Arabic (Iraq)" },
                    { "ar-JO", "Arabic (Jordan)" },
                    { "ar-KM", "Arabic (Comoros)" },
                    { "ar-KW", "Arabic (Kuwait)" },
                    { "ar-LB", "Arabic (Lebanon)" },
                    { "ar-LY", "Arabic (Libya)" },
                    { "ar-MA", "Arabic (Morocco)" },
                    { "ar-MR", "Arabic (Mauritania)" },
                    { "ar-OM", "Arabic (Oman)" },
                    { "ar-PS", "Arabic (Palestinian Authority)" },
                    { "ar-QA", "Arabic (Qatar)" },
                    { "ar-SA", "Arabic (Saudi Arabia)" },
                    { "ar-SD", "Arabic (Sudan)" },
                    { "ar-SO", "Arabic (Somalia)" },
                    { "ar-SS", "Arabic (South Sudan)" },
                    { "ar-SY", "Arabic (Syria)" },
                    { "ar-TD", "Arabic (Chad)" },
                    { "ar-TN", "Arabic (Tunisia)" },
                    { "ar-YE", "Arabic (Yemen)" },
                    { "arn", "Mapuche" },
                    { "arn-CL", "Mapuche (Chile)" },
                    { "as", "Assamese" },
                    { "as-IN", "Assamese (India)" },
                    { "asa", "Asu" },
                    { "asa-TZ", "Asu (Tanzania)" },
                    { "ast", "Asturian" },
                    { "ast-ES", "Asturian (Spain)" },
                    { "az-Cyrl", "Azerbaijani (Cyrillic)" },
                    { "az-Cyrl-AZ", "Azerbaijani (Cyrillic, Azerbaijan)" },
                    { "az-Latn", "Azerbaijani (Latin)" },
                    { "az-Latn-AZ", "Azerbaijani (Latin, Azerbaijan)" },
                    { "ba", "Bashkir" },
                    { "ba-RU", "Bashkir (Russia)" },
                    { "bas", "Basaa" },
                    { "bas-CM", "Basaa (Cameroon)" },
                    { "be", "Belarusian" },
                    { "be-BY", "Belarusian (Belarus)" },
                    { "bem", "Bemba" },
                    { "bem-ZM", "Bemba (Zambia)" },
                    { "bez", "Bena" },
                    { "bez-TZ", "Bena (Tanzania)" },
                    { "bg", "Bulgarian" },
                    { "bg-BG", "Bulgarian (Bulgaria)" },
                    { "bgc", "Haryanvi" },
                    { "bgc-IN", "Haryanvi (India)" },
                    { "bho-IN", "Bhojpuri (India)" },
                    { "bin", "Edo" },
                    { "bin-NG", "Edo (Nigeria)" },
                    { "bm", "Bamanankan" },
                    { "bm-ML", "Bamanankan (Mali)" },
                    { "bn-BD", "Bangla (Bangladesh)" },
                    { "bn-IN", "Bangla (India)" },
                    { "bo", "Tibetan" },
                    { "bo-CN", "Tibetan (China)" },
                    { "bo-IN", "Tibetan (India)" },
                    { "br", "Breton" },
                    { "br-FR", "Breton (France)" },
                    { "brx", "Bodo" },
                    { "brx-IN", "Bodo (India)" },
                    { "bs", "Bosnian" },
                    { "bs-Cyrl", "Bosnian (Cyrillic)" },
                    { "bs-Cyrl-BA", "Bosnian (Cyrillic, Bosnia & Herzegovina)" },
                    { "bs-Latn", "Bosnian (Latin)" },
                    { "bs-Latn-BA", "Bosnian (Latin, Bosnia & Herzegovina)" },
                    { "byn", "Blin" },
                    { "byn-ER", "Blin (Eritrea)" },
                    { "ca", "Catalan" },
                    { "ca-AD", "Catalan (Andorra)" },
                    { "ca-ES", "Catalan (Spain)" },
                    { "ca-FR", "Catalan (France)" },
                    { "ca-IT", "Catalan (Italy)" },
                    { "ccp", "Chakma" },
                    { "ccp-BD", "Chakma (Bangladesh)" },
                    { "ccp-IN", "Chakma (India)" },
                    { "ce", "Chechen" },
                    { "ce-RU", "Chechen (Russia)" },
                    { "ceb-PH", "Cebuano (Philippines)" },
                    { "cgg", "Chiga" },
                    { "cgg-UG", "Chiga (Uganda)" },
                    { "chr", "Cherokee" },
                    { "chr-US", "Cherokee (United States)" },
                    { "ckb", "Central Kurdish" },
                    { "ckb-IQ", "Central Kurdish (Iraq)" },
                    { "ckb-IR", "Central Kurdish (Iran)" },
                    { "co", "Corsican" },
                    { "co-FR", "Corsican (France)" },
                    { "cs", "Czech" },
                    { "cs-CZ", "Czech (Czechia)" },
                    { "cu", "Church Slavic" },
                    { "cu-RU", "Church Slavic (Russia)" },
                    { "cv", "Chuvash" },
                    { "cv-RU", "Chuvash (Russia)" },
                    { "cy", "Welsh" },
                    { "cy-GB", "Welsh (United Kingdom)" },
                    { "da", "Danish" },
                    { "da-DK", "Danish (Denmark)" },
                    { "da-GL", "Danish (Greenland)" },
                    { "dav", "Taita" },
                    { "dav-KE", "Taita (Kenya)" },
                    { "de-AT", "German (Austria)" },
                    { "de-BE", "German (Belgium)" },
                    { "de-CH", "German (Switzerland)" },
                    { "de-DE", "German (Germany)" },
                    { "de-IT", "German (Italy)" },
                    { "de-LI", "German (Liechtenstein)" },
                    { "de-LU", "German (Luxembourg)" },
                    { "dje", "Zarma" },
                    { "dje-NE", "Zarma (Niger)" },
                    { "doi", "Dogri" },
                    { "doi-IN", "Dogri (India)" },
                    { "dsb", "Lower Sorbian" },
                    { "dsb-DE", "Lower Sorbian (Germany)" },
                    { "dua", "Duala" },
                    { "dua-CM", "Duala (Cameroon)" },
                    { "dv", "Divehi" },
                    { "dv-MV", "Divehi (Maldives)" },
                    { "dyo", "Jola-Fonyi" },
                    { "dyo-SN", "Jola-Fonyi (Senegal)" },
                    { "dz", "Dzongkha" },
                    { "dz-BT", "Dzongkha (Bhutan)" },
                    { "ebu", "Embu" },
                    { "ebu-KE", "Embu (Kenya)" },
                    { "ee", "Ewe" },
                    { "ee-GH", "Ewe (Ghana)" },
                    { "ee-TG", "Ewe (Togo)" },
                    { "el", "Greek" },
                    { "el-CY", "Greek (Cyprus)" },
                    { "el-GR", "Greek (Greece)" },
                    { "en-001", "English (world)" },
                    { "en-029", "English (Caribbean)" },
                    { "en-150", "English (Europe)" },
                    { "en-AE", "English (United Arab Emirates)" },
                    { "en-AG", "English (Antigua & Barbuda)" },
                    { "en-AI", "English (Anguilla)" },
                    { "en-AS", "English (American Samoa)" },
                    { "en-AT", "English (Austria)" },
                    { "en-AU", "English (Australia)" },
                    { "en-BB", "English (Barbados)" },
                    { "en-BE", "English (Belgium)" },
                    { "en-BI", "English (Burundi)" },
                    { "en-BM", "English (Bermuda)" },
                    { "en-BS", "English (Bahamas)" },
                    { "en-BW", "English (Botswana)" },
                    { "en-BZ", "English (Belize)" },
                    { "en-CA", "English (Canada)" },
                    { "en-CC", "English (Cocos [Keeling] Islands)" },
                    { "en-CH", "English (Switzerland)" },
                    { "en-CK", "English (Cook Islands)" },
                    { "en-CM", "English (Cameroon)" },
                    { "en-CX", "English (Christmas Island)" },
                    { "en-CY", "English (Cyprus)" },
                    { "en-DE", "English (Germany)" },
                    { "en-DK", "English (Denmark)" },
                    { "en-DM", "English (Dominica)" },
                    { "en-ER", "English (Eritrea)" },
                    { "en-FI", "English (Finland)" },
                    { "en-FJ", "English (Fiji)" },
                    { "en-FK", "English (Falkland Islands)" },
                    { "en-FM", "English (Micronesia)" },
                    { "en-GB", "English (United Kingdom)" },
                    { "en-GD", "English (Grenada)" },
                    { "en-GG", "English (Guernsey)" },
                    { "en-GH", "English (Ghana)" },
                    { "en-GI", "English (Gibraltar)" },
                    { "en-GM", "English (Gambia)" },
                    { "en-GU", "English (Guam)" },
                    { "en-GY", "English (Guyana)" },
                    { "en-HK", "English (Hong Kong SAR)" },
                    { "en-ID", "English (Indonesia)" },
                    { "en-IE", "English (Ireland)" },
                    { "en-IL", "English (Israel)" },
                    { "en-IM", "English (Isle of Man)" },
                    { "en-IN", "English (India)" },
                    { "en-IO", "English (British Indian Ocean Territory)" },
                    { "en-JE", "English (Jersey)" },
                    { "en-JM", "English (Jamaica)" },
                    { "en-KE", "English (Kenya)" },
                    { "en-KI", "English (Kiribati)" },
                    { "en-KN", "English (St. Kitts & Nevis)" },
                    { "en-KY", "English (Cayman Islands)" },
                    { "en-LC", "English (St. Lucia)" },
                    { "en-LR", "English (Liberia)" },
                    { "en-LS", "English (Lesotho)" },
                    { "en-MG", "English (Madagascar)" },
                    { "en-MH", "English (Marshall Islands)" },
                    { "en-MO", "English (Macao SAR)" },
                    { "en-MP", "English (Northern Mariana Islands)" },
                    { "en-MS", "English (Montserrat)" },
                    { "en-MT", "English (Malta)" },
                    { "en-MU", "English (Mauritius)" },
                    { "en-MV", "English (Maldives)" },
                    { "en-MW", "English (Malawi)" },
                    { "en-MY", "English (Malaysia)" },
                    { "en-NA", "English (Namibia)" },
                    { "en-NF", "English (Norfolk Island)" },
                    { "en-NG", "English (Nigeria)" },
                    { "en-NL", "English (Netherlands)" },
                    { "en-NR", "English (Nauru)" },
                    { "en-NU", "English (Niue)" },
                    { "en-NZ", "English (New Zealand)" },
                    { "en-PG", "English (Papua New Guinea)" },
                    { "en-PH", "English (Philippines)" },
                    { "en-PK", "English (Pakistan)" },
                    { "en-PN", "English (Pitcairn Islands)" },
                    { "en-PR", "English (Puerto Rico)" },
                    { "en-PW", "English (Palau)" },
                    { "en-RW", "English (Rwanda)" },
                    { "en-SB", "English (Solomon Islands)" },
                    { "en-SC", "English (Seychelles)" },
                    { "en-SD", "English (Sudan)" },
                    { "en-SE", "English (Sweden)" },
                    { "en-SG", "English (Singapore)" },
                    { "en-SH", "English (St Helena, Ascension, Tristan da Cunha)" },
                    { "en-SI", "English (Slovenia)" },
                    { "en-SL", "English (Sierra Leone)" },
                    { "en-SS", "English (South Sudan)" },
                    { "en-SX", "English (Sint Maarten)" },
                    { "en-SZ", "English (Eswatini)" },
                    { "en-TC", "English (Turks & Caicos Islands)" },
                    { "en-TK", "English (Tokelau)" },
                    { "en-TO", "English (Tonga)" },
                    { "en-TT", "English (Trinidad & Tobago)" },
                    { "en-TV", "English (Tuvalu)" },
                    { "en-TZ", "English (Tanzania)" },
                    { "en-UG", "English (Uganda)" },
                    { "en-UM", "English (U.S. Outlying Islands)" },
                    { "en-US", "English (United States)" },
                    { "en-US-POSIX", "English (United States, Computer)" },
                    { "en-VC", "English (St. Vincent & Grenadines)" },
                    { "en-VG", "English (British Virgin Islands)" },
                    { "en-VI", "English (U.S. Virgin Islands)" },
                    { "en-VU", "English (Vanuatu)" },
                    { "en-WS", "English (Samoa)" },
                    { "en-ZA", "English (South Africa)" },
                    { "en-ZM", "English (Zambia)" },
                    { "en-ZW", "English (Zimbabwe)" },
                    { "eo", "Esperanto" },
                    { "eo-001", "Esperanto (world)" },
                    { "es-419", "Spanish (Latin America)" },
                    { "es-AR", "Spanish (Argentina)" },
                    { "es-BO", "Spanish (Bolivia)" },
                    { "es-BR", "Spanish (Brazil)" },
                    { "es-BZ", "Spanish (Belize)" },
                    { "es-CL", "Spanish (Chile)" },
                    { "es-CO", "Spanish (Colombia)" },
                    { "es-CR", "Spanish (Costa Rica)" },
                    { "es-CU", "Spanish (Cuba)" },
                    { "es-DO", "Spanish (Dominican Republic)" },
                    { "es-EC", "Spanish (Ecuador)" },
                    { "es-ES", "Spanish (Spain)" },
                    { "es-GQ", "Spanish (Equatorial Guinea)" },
                    { "es-GT", "Spanish (Guatemala)" },
                    { "es-HN", "Spanish (Honduras)" },
                    { "es-MX", "Spanish (Mexico)" },
                    { "es-NI", "Spanish (Nicaragua)" },
                    { "es-PA", "Spanish (Panama)" },
                    { "es-PE", "Spanish (Peru)" },
                    { "es-PH", "Spanish (Philippines)" },
                    { "es-PR", "Spanish (Puerto Rico)" },
                    { "es-PY", "Spanish (Paraguay)" },
                    { "es-SV", "Spanish (El Salvador)" },
                    { "es-US", "Spanish (United States)" },
                    { "es-UY", "Spanish (Uruguay)" },
                    { "es-VE", "Spanish (Venezuela)" },
                    { "et", "Estonian" },
                    { "et-EE", "Estonian (Estonia)" },
                    { "eu", "Basque" },
                    { "eu-ES", "Basque (Spain)" },
                    { "ewo", "Ewondo" },
                    { "ewo-CM", "Ewondo (Cameroon)" },
                    { "fa-AF", "Persian (Afghanistan)" },
                    { "fa-IR", "Persian (Iran)" },
                    { "ff", "Fula" },
                    { "ff-Adlm", "Fula (Adlam)" },
                    { "ff-Adlm-BF", "Fula (Adlam, Burkina Faso)" },
                    { "ff-Adlm-CM", "Fula (Adlam, Cameroon)" },
                    { "ff-Adlm-GH", "Fula (Adlam, Ghana)" },
                    { "ff-Adlm-GM", "Fula (Adlam, Gambia)" },
                    { "ff-Adlm-GN", "Fula (Adlam, Guinea)" },
                    { "ff-Adlm-GW", "Fula (Adlam, Guinea-Bissau)" },
                    { "ff-Adlm-LR", "Fula (Adlam, Liberia)" },
                    { "ff-Adlm-MR", "Fula (Adlam, Mauritania)" },
                    { "ff-Adlm-NE", "Fula (Adlam, Niger)" },
                    { "ff-Adlm-NG", "Fula (Adlam, Nigeria)" },
                    { "ff-Adlm-SL", "Fula (Adlam, Sierra Leone)" },
                    { "ff-Adlm-SN", "Fula (Adlam, Senegal)" },
                    { "ff-Latn", "Fula (Latin)" },
                    { "ff-Latn-BF", "Fula (Latin, Burkina Faso)" },
                    { "ff-Latn-CM", "Fula (Latin, Cameroon)" },
                    { "ff-Latn-GH", "Fula (Latin, Ghana)" },
                    { "ff-Latn-GM", "Fula (Latin, Gambia)" },
                    { "ff-Latn-GN", "Fula (Latin, Guinea)" },
                    { "ff-Latn-GW", "Fula (Latin, Guinea-Bissau)" },
                    { "ff-Latn-LR", "Fula (Latin, Liberia)" },
                    { "ff-Latn-MR", "Fula (Latin, Mauritania)" },
                    { "ff-Latn-NE", "Fula (Latin, Niger)" },
                    { "ff-Latn-NG", "Fula (Latin, Nigeria)" },
                    { "ff-Latn-SL", "Fula (Latin, Sierra Leone)" },
                    { "ff-Latn-SN", "Fula (Latin, Senegal)" },
                    { "fi", "Finnish" },
                    { "fi-FI", "Finnish (Finland)" },
                    { "fil", "Filipino" },
                    { "fil-PH", "Filipino (Philippines)" },
                    { "fo", "Faroese" },
                    { "fo-DK", "Faroese (Denmark)" },
                    { "fo-FO", "Faroese (Faroe Islands)" },
                    { "fr-029", "French (Caribbean)" },
                    { "fr-BE", "French (Belgium)" },
                    { "fr-BF", "French (Burkina Faso)" },
                    { "fr-BI", "French (Burundi)" },
                    { "fr-BJ", "French (Benin)" },
                    { "fr-BL", "French (St. Barthélemy)" },
                    { "fr-CA", "French (Canada)" },
                    { "fr-CD", "French (Congo [DRC])" },
                    { "fr-CF", "French (Central African Republic)" },
                    { "fr-CG", "French (Congo)" },
                    { "fr-CH", "French (Switzerland)" },
                    { "fr-CI", "French (Côte d’Ivoire)" },
                    { "fr-CM", "French (Cameroon)" },
                    { "fr-DJ", "French (Djibouti)" },
                    { "fr-DZ", "French (Algeria)" },
                    { "fr-FR", "French (France)" },
                    { "fr-GA", "French (Gabon)" },
                    { "fr-GF", "French (French Guiana)" },
                    { "fr-GN", "French (Guinea)" },
                    { "fr-GP", "French (Guadeloupe)" },
                    { "fr-GQ", "French (Equatorial Guinea)" },
                    { "fr-HT", "French (Haiti)" },
                    { "fr-KM", "French (Comoros)" },
                    { "fr-LU", "French (Luxembourg)" },
                    { "fr-MA", "French (Morocco)" },
                    { "fr-MC", "French (Monaco)" },
                    { "fr-MF", "French (St. Martin)" },
                    { "fr-MG", "French (Madagascar)" },
                    { "fr-ML", "French (Mali)" },
                    { "fr-MQ", "French (Martinique)" },
                    { "fr-MR", "French (Mauritania)" },
                    { "fr-MU", "French (Mauritius)" },
                    { "fr-NC", "French (New Caledonia)" },
                    { "fr-NE", "French (Niger)" },
                    { "fr-PF", "French (French Polynesia)" },
                    { "fr-PM", "French (St. Pierre & Miquelon)" },
                    { "fr-RE", "French (Réunion)" },
                    { "fr-RW", "French (Rwanda)" },
                    { "fr-SC", "French (Seychelles)" },
                    { "fr-SN", "French (Senegal)" },
                    { "fr-SY", "French (Syria)" },
                    { "fr-TD", "French (Chad)" },
                    { "fr-TG", "French (Togo)" },
                    { "fr-TN", "French (Tunisia)" },
                    { "fr-VU", "French (Vanuatu)" },
                    { "fr-WF", "French (Wallis & Futuna)" },
                    { "fr-YT", "French (Mayotte)" },
                    { "fur", "Friulian" },
                    { "fur-IT", "Friulian (Italy)" },
                    { "fy", "Western Frisian" },
                    { "fy-NL", "Western Frisian (Netherlands)" },
                    { "ga", "Irish" },
                    { "ga-GB", "Irish (United Kingdom)" },
                    { "ga-IE", "Irish (Ireland)" },
                    { "gd", "Scottish Gaelic" },
                    { "gd-GB", "Scottish Gaelic (United Kingdom)" },
                    { "gl", "Galician" },
                    { "gl-ES", "Galician (Spain)" },
                    { "gn", "Guarani" },
                    { "gn-PY", "Guarani (Paraguay)" },
                    { "gsw", "Swiss German" },
                    { "gsw-CH", "Swiss German (Switzerland)" },
                    { "gsw-FR", "Swiss German (France)" },
                    { "gsw-LI", "Swiss German (Liechtenstein)" },
                    { "gu-IN", "Gujarati (India)" },
                    { "guz", "Gusii" },
                    { "guz-KE", "Gusii (Kenya)" },
                    { "gv", "Manx" },
                    { "gv-IM", "Manx (Isle of Man)" },
                    { "ha-GH", "Hausa (Ghana)" },
                    { "ha-NE", "Hausa (Niger)" },
                    { "ha-NG", "Hausa (Nigeria)" },
                    { "haw", "Hawaiian" },
                    { "haw-US", "Hawaiian (United States)" },
                    { "he", "Hebrew" },
                    { "he-IL", "Hebrew (Israel)" },
                    { "hi-IN", "Hindi (India)" },
                    { "hi-Latn", "Hindi (Latin)" },
                    { "hi-Latn-IN", "Hindi (Latin, India)" },
                    { "hr", "Croatian" },
                    { "hr-BA", "Croatian (Bosnia & Herzegovina)" },
                    { "hr-HR", "Croatian (Croatia)" },
                    { "hsb", "Upper Sorbian" },
                    { "hsb-DE", "Upper Sorbian (Germany)" },
                    { "hu", "Hungarian" },
                    { "hu-HU", "Hungarian (Hungary)" },
                    { "hy", "Armenian" },
                    { "hy-AM", "Armenian (Armenia)" },
                    { "ia", "Interlingua" },
                    { "ia-001", "Interlingua (world)" },
                    { "ibb", "Ibibio" },
                    { "ibb-NG", "Ibibio (Nigeria)" },
                    { "id-ID", "Indonesian (Indonesia)" },
                    { "ig", "Igbo" },
                    { "ig-NG", "Igbo (Nigeria)" },
                    { "ii", "Yi" },
                    { "ii-CN", "Yi (China)" },
                    { "is", "Icelandic" },
                    { "is-IS", "Icelandic (Iceland)" },
                    { "it-CH", "Italian (Switzerland)" },
                    { "it-IT", "Italian (Italy)" },
                    { "it-SM", "Italian (San Marino)" },
                    { "it-VA", "Italian (Vatican City)" },
                    { "iu", "Inuktitut" },
                    { "iu-CA", "Inuktitut (Canada)" },
                    { "iu-Latn", "Inuktitut (Latin)" },
                    { "iu-Latn-CA", "Inuktitut (Latin, Canada)" },
                    { "ja-JP", "Japanese (Japan)" },
                    { "jgo", "Ngomba" },
                    { "jgo-CM", "Ngomba (Cameroon)" },
                    { "jmc", "Machame" },
                    { "jmc-TZ", "Machame (Tanzania)" },
                    { "jv-ID", "Javanese (Indonesia)" },
                    { "jv-Java", "Javanese (Javanese)" },
                    { "jv-Java-ID", "Javanese (Javanese, Indonesia)" },
                    { "ka", "Georgian" },
                    { "ka-GE", "Georgian (Georgia)" },
                    { "kab", "Kabyle" },
                    { "kab-DZ", "Kabyle (Algeria)" },
                    { "kam", "Kamba" },
                    { "kam-KE", "Kamba (Kenya)" },
                    { "kde", "Makonde" },
                    { "kde-TZ", "Makonde (Tanzania)" },
                    { "kea", "Kabuverdianu" },
                    { "kea-CV", "Kabuverdianu (Cabo Verde)" },
                    { "kgp", "Kaingang" },
                    { "kgp-BR", "Kaingang (Brazil)" },
                    { "khq", "Koyra Chiini" },
                    { "khq-ML", "Koyra Chiini (Mali)" },
                    { "ki", "Kikuyu" },
                    { "ki-KE", "Kikuyu (Kenya)" },
                    { "kk", "Kazakh" },
                    { "kk-KZ", "Kazakh (Kazakhstan)" },
                    { "kkj", "Kako" },
                    { "kkj-CM", "Kako (Cameroon)" },
                    { "kl", "Kalaallisut" },
                    { "kl-GL", "Kalaallisut (Greenland)" },
                    { "kln", "Kalenjin" },
                    { "kln-KE", "Kalenjin (Kenya)" },
                    { "km-KH", "Khmer (Cambodia)" },
                    { "kn-IN", "Kannada (India)" },
                    { "ko-KP", "Korean (North Korea)" },
                    { "ko-KR", "Korean (Korea)" },
                    { "kok", "Konkani" },
                    { "kok-IN", "Konkani (India)" },
                    { "kr", "Kanuri" },
                    { "kr-Latn", "Kanuri (Latin)" },
                    { "kr-Latn-NG", "Kanuri (Latin, Nigeria)" },
                    { "ks", "Kashmiri" },
                    { "ks-Arab", "Kashmiri (Arabic)" },
                    { "ks-Arab-IN", "Kashmiri (Arabic, India)" },
                    { "ks-Deva", "Kashmiri (Devanagari)" },
                    { "ks-Deva-IN", "Kashmiri (Devanagari, India)" },
                    { "ksb", "Shambala" },
                    { "ksb-TZ", "Shambala (Tanzania)" },
                    { "ksf", "Bafia" },
                    { "ksf-CM", "Bafia (Cameroon)" },
                    { "ksh", "Colognian" },
                    { "ksh-DE", "Colognian (Germany)" },
                    { "kw", "Cornish" },
                    { "kw-GB", "Cornish (United Kingdom)" },
                    { "ky", "Kyrgyz" },
                    { "ky-KG", "Kyrgyz (Kyrgyzstan)" },
                    { "la", "Latin" },
                    { "la-VA", "Latin (Vatican City)" },
                    { "lag", "Langi" },
                    { "lag-TZ", "Langi (Tanzania)" },
                    { "lb", "Luxembourgish" },
                    { "lb-LU", "Luxembourgish (Luxembourg)" },
                    { "lg", "Ganda" },
                    { "lg-UG", "Ganda (Uganda)" },
                    { "lkt", "Lakota" },
                    { "lkt-US", "Lakota (United States)" },
                    { "ln", "Lingala" },
                    { "ln-AO", "Lingala (Angola)" },
                    { "ln-CD", "Lingala (Congo [DRC])" },
                    { "ln-CF", "Lingala (Central African Republic)" },
                    { "ln-CG", "Lingala (Congo)" },
                    { "lo", "Lao" },
                    { "lo-LA", "Lao (Laos)" },
                    { "lrc", "Northern Luri" },
                    { "lrc-IQ", "Northern Luri (Iraq)" },
                    { "lrc-IR", "Northern Luri (Iran)" },
                    { "lt", "Lithuanian" },
                    { "lt-LT", "Lithuanian (Lithuania)" },
                    { "lu", "Luba-Katanga" },
                    { "lu-CD", "Luba-Katanga (Congo [DRC])" },
                    { "luo", "Luo" },
                    { "luo-KE", "Luo (Kenya)" },
                    { "luy", "Luyia" },
                    { "luy-KE", "Luyia (Kenya)" },
                    { "lv", "Latvian" },
                    { "lv-LV", "Latvian (Latvia)" },
                    { "mai-IN", "Maithili (India)" },
                    { "mas", "Masai" },
                    { "mas-KE", "Masai (Kenya)" },
                    { "mas-TZ", "Masai (Tanzania)" },
                    { "mer", "Meru" },
                    { "mer-KE", "Meru (Kenya)" },
                    { "mfe", "Morisyen" },
                    { "mfe-MU", "Morisyen (Mauritius)" },
                    { "mg", "Malagasy" },
                    { "mg-MG", "Malagasy (Madagascar)" },
                    { "mgh", "Makhuwa-Meetto" },
                    { "mgh-MZ", "Makhuwa-Meetto (Mozambique)" },
                    { "mgo", "Metaʼ" },
                    { "mgo-CM", "Metaʼ (Cameroon)" },
                    { "mi", "Māori" },
                    { "mi-NZ", "Māori (New Zealand)" },
                    { "mk", "Macedonian" },
                    { "mk-MK", "Macedonian (North Macedonia)" },
                    { "ml-IN", "Malayalam (India)" },
                    { "mn", "Mongolian" },
                    { "mn-MN", "Mongolian (Mongolia)" },
                    { "mn-Mong", "Mongolian (Mongolian)" },
                    { "mn-Mong-CN", "Mongolian (Mongolian, China)" },
                    { "mn-Mong-MN", "Mongolian (Mongolian, Mongolia)" },
                    { "mni", "Manipuri" },
                    { "mni-Beng", "Manipuri (Bangla)" },
                    { "mni-Beng-IN", "Manipuri (Bangla, India)" },
                    { "moh", "Mohawk" },
                    { "moh-CA", "Mohawk (Canada)" },
                    { "mr-IN", "Marathi (India)" },
                    { "ms-BN", "Malay (Brunei)" },
                    { "ms-ID", "Malay (Indonesia)" },
                    { "ms-MY", "Malay (Malaysia)" },
                    { "ms-SG", "Malay (Singapore)" },
                    { "mt", "Maltese" },
                    { "mt-MT", "Maltese (Malta)" },
                    { "mua", "Mundang" },
                    { "mua-CM", "Mundang (Cameroon)" },
                    { "my-MM", "Burmese (Myanmar)" },
                    { "mzn", "Mazanderani" },
                    { "mzn-IR", "Mazanderani (Iran)" },
                    { "naq", "Nama" },
                    { "naq-NA", "Nama (Namibia)" },
                    { "nb", "Norwegian Bokmål" },
                    { "nb-NO", "Norwegian Bokmål (Norway)" },
                    { "nb-SJ", "Norwegian Bokmål (Svalbard & Jan Mayen)" },
                    { "nd", "North Ndebele" },
                    { "nd-ZW", "North Ndebele (Zimbabwe)" },
                    { "nds", "Low German" },
                    { "nds-DE", "Low German (Germany)" },
                    { "nds-NL", "Low German (Netherlands)" },
                    { "ne-IN", "Nepali (India)" },
                    { "ne-NP", "Nepali (Nepal)" },
                    { "nl-AW", "Dutch (Aruba)" },
                    { "nl-BE", "Dutch (Belgium)" },
                    { "nl-BQ", "Dutch (Bonaire, Sint Eustatius and Saba)" },
                    { "nl-CW", "Dutch (Curaçao)" },
                    { "nl-NL", "Dutch (Netherlands)" },
                    { "nl-SR", "Dutch (Suriname)" },
                    { "nl-SX", "Dutch (Sint Maarten)" },
                    { "nmg", "Kwasio" },
                    { "nmg-CM", "Kwasio (Cameroon)" },
                    { "nn", "Norwegian Nynorsk" },
                    { "nn-NO", "Norwegian Nynorsk (Norway)" },
                    { "nnh", "Ngiemboon" },
                    { "nnh-CM", "Ngiemboon (Cameroon)" },
                    { "no", "Norwegian" },
                    { "nqo", "N’Ko" },
                    { "nqo-GN", "N’Ko (Guinea)" },
                    { "nr", "South Ndebele" },
                    { "nr-ZA", "South Ndebele (South Africa)" },
                    { "nso", "Sesotho sa Leboa" },
                    { "nso-ZA", "Sesotho sa Leboa (South Africa)" },
                    { "nus", "Nuer" },
                    { "nus-SS", "Nuer (South Sudan)" },
                    { "nyn", "Nyankole" },
                    { "nyn-UG", "Nyankole (Uganda)" },
                    { "oc", "Occitan" },
                    { "oc-ES", "Occitan (Spain)" },
                    { "oc-FR", "Occitan (France)" },
                    { "om", "Oromo" },
                    { "om-ET", "Oromo (Ethiopia)" },
                    { "om-KE", "Oromo (Kenya)" },
                    { "or-IN", "Odia (India)" },
                    { "os", "Ossetic" },
                    { "os-GE", "Ossetic (Georgia)" },
                    { "os-RU", "Ossetic (Russia)" },
                    { "pa-Arab", "Punjabi (Arabic)" },
                    { "pa-Arab-PK", "Punjabi (Arabic, Pakistan)" },
                    { "pa-Guru", "Punjabi (Gurmukhi)" },
                    { "pa-Guru-IN", "Punjabi (Gurmukhi, India)" },
                    { "pap", "Papiamento" },
                    { "pap-029", "Papiamento (Caribbean)" },
                    { "pcm", "Nigerian Pidgin" },
                    { "pcm-NG", "Nigerian Pidgin (Nigeria)" },
                    { "pl-PL", "Polish (Poland)" },
                    { "prg", "Prussian" },
                    { "prg-001", "Prussian (world)" },
                    { "ps", "Pashto" },
                    { "ps-AF", "Pashto (Afghanistan)" },
                    { "ps-PK", "Pashto (Pakistan)" },
                    { "pt-AO", "Portuguese (Angola)" },
                    { "pt-BR", "Portuguese (Brazil)" },
                    { "pt-CH", "Portuguese (Switzerland)" },
                    { "pt-CV", "Portuguese (Cabo Verde)" },
                    { "pt-GQ", "Portuguese (Equatorial Guinea)" },
                    { "pt-GW", "Portuguese (Guinea-Bissau)" },
                    { "pt-LU", "Portuguese (Luxembourg)" },
                    { "pt-MO", "Portuguese (Macao SAR)" },
                    { "pt-MZ", "Portuguese (Mozambique)" },
                    { "pt-PT", "Portuguese (Portugal)" },
                    { "pt-ST", "Portuguese (São Tomé & Príncipe)" },
                    { "pt-TL", "Portuguese (Timor-Leste)" },
                    { "qu", "Quechua" },
                    { "qu-BO", "Quechua (Bolivia)" },
                    { "qu-EC", "Quechua (Ecuador)" },
                    { "qu-PE", "Quechua (Peru)" },
                    { "quc", "Kʼicheʼ" },
                    { "quc-GT", "Kʼicheʼ (Guatemala)" },
                    { "raj", "Rajasthani" },
                    { "raj-IN", "Rajasthani (India)" },
                    { "rm", "Romansh" },
                    { "rm-CH", "Romansh (Switzerland)" },
                    { "rn", "Rundi" },
                    { "rn-BI", "Rundi (Burundi)" },
                    { "ro-MD", "Romanian (Moldova)" },
                    { "ro-RO", "Romanian (Romania)" },
                    { "rof", "Rombo" },
                    { "rof-TZ", "Rombo (Tanzania)" },
                    { "ru-BY", "Russian (Belarus)" },
                    { "ru-KG", "Russian (Kyrgyzstan)" },
                    { "ru-KZ", "Russian (Kazakhstan)" },
                    { "ru-MD", "Russian (Moldova)" },
                    { "ru-RU", "Russian (Russia)" },
                    { "ru-UA", "Russian (Ukraine)" },
                    { "rw", "Kinyarwanda" },
                    { "rw-RW", "Kinyarwanda (Rwanda)" },
                    { "rwk", "Rwa" },
                    { "rwk-TZ", "Rwa (Tanzania)" },
                    { "sa", "Sanskrit" },
                    { "sa-IN", "Sanskrit (India)" },
                    { "sah", "Yakut" },
                    { "sah-RU", "Yakut (Russia)" },
                    { "saq", "Samburu" },
                    { "saq-KE", "Samburu (Kenya)" },
                    { "sat", "Santali" },
                    { "sat-Olck", "Santali (Ol Chiki)" },
                    { "sat-Olck-IN", "Santali (Ol Chiki, India)" },
                    { "sbp", "Sangu" },
                    { "sbp-TZ", "Sangu (Tanzania)" },
                    { "sc", "Sardinian" },
                    { "sc-IT", "Sardinian (Italy)" },
                    { "sd-Arab", "Sindhi (Arabic)" },
                    { "sd-Arab-PK", "Sindhi (Arabic, Pakistan)" },
                    { "sd-Deva", "Sindhi (Devanagari)" },
                    { "sd-Deva-IN", "Sindhi (Devanagari, India)" },
                    { "se", "Northern Sami" },
                    { "se-FI", "Northern Sami (Finland)" },
                    { "se-NO", "Northern Sami (Norway)" },
                    { "se-SE", "Northern Sami (Sweden)" },
                    { "seh", "Sena" },
                    { "seh-MZ", "Sena (Mozambique)" },
                    { "ses", "Koyraboro Senni" },
                    { "ses-ML", "Koyraboro Senni (Mali)" },
                    { "sg", "Sango" },
                    { "sg-CF", "Sango (Central African Republic)" },
                    { "shi", "Tachelhit" },
                    { "shi-Latn", "Tachelhit (Latin)" },
                    { "shi-Latn-MA", "Tachelhit (Latin, Morocco)" },
                    { "shi-Tfng", "Tachelhit (Tifinagh)" },
                    { "shi-Tfng-MA", "Tachelhit (Tifinagh, Morocco)" },
                    { "si-LK", "Sinhala (Sri Lanka)" },
                    { "sk", "Slovak" },
                    { "sk-SK", "Slovak (Slovakia)" },
                    { "sl", "Slovenian" },
                    { "sl-SI", "Slovenian (Slovenia)" },
                    { "sma", "Southern Sami" },
                    { "sma-NO", "Southern Sami (Norway)" },
                    { "sma-SE", "Southern Sami (Sweden)" },
                    { "smj", "Lule Sami" },
                    { "smj-NO", "Lule Sami (Norway)" },
                    { "smj-SE", "Lule Sami (Sweden)" },
                    { "smn", "Inari Sami" },
                    { "smn-FI", "Inari Sami (Finland)" },
                    { "sms", "Skolt Sami" },
                    { "sms-FI", "Skolt Sami (Finland)" },
                    { "sn", "Shona" },
                    { "sn-ZW", "Shona (Zimbabwe)" },
                    { "so", "Somali" },
                    { "so-DJ", "Somali (Djibouti)" },
                    { "so-ET", "Somali (Ethiopia)" },
                    { "so-KE", "Somali (Kenya)" },
                    { "so-SO", "Somali (Somalia)" },
                    { "sq", "Albanian" },
                    { "sq-AL", "Albanian (Albania)" },
                    { "sq-MK", "Albanian (North Macedonia)" },
                    { "sq-XK", "Albanian (Kosovo)" },
                    { "sr", "Serbian" },
                    { "sr-Cyrl", "Serbian (Cyrillic)" },
                    { "sr-Cyrl-BA", "Serbian (Cyrillic, Bosnia & Herzegovina)" },
                    { "sr-Cyrl-ME", "Serbian (Cyrillic, Montenegro)" },
                    { "sr-Cyrl-RS", "Serbian (Cyrillic, Serbia)" },
                    { "sr-Cyrl-XK", "Serbian (Cyrillic, Kosovo)" },
                    { "sr-Latn", "Serbian (Latin)" },
                    { "sr-Latn-BA", "Serbian (Latin, Bosnia & Herzegovina)" },
                    { "sr-Latn-ME", "Serbian (Latin, Montenegro)" },
                    { "sr-Latn-RS", "Serbian (Latin, Serbia)" },
                    { "sr-Latn-XK", "Serbian (Latin, Kosovo)" },
                    { "ss", "siSwati" },
                    { "ss-SZ", "siSwati (Eswatini)" },
                    { "ss-ZA", "siSwati (South Africa)" },
                    { "ssy", "Saho" },
                    { "ssy-ER", "Saho (Eritrea)" },
                    { "st", "Sesotho" },
                    { "st-LS", "Sesotho (Lesotho)" },
                    { "st-ZA", "Sesotho (South Africa)" },
                    { "su-Latn", "Sundanese (Latin)" },
                    { "su-Latn-ID", "Sundanese (Latin, Indonesia)" },
                    { "sv", "Swedish" },
                    { "sv-AX", "Swedish (Åland Islands)" },
                    { "sv-FI", "Swedish (Finland)" },
                    { "sv-SE", "Swedish (Sweden)" },
                    { "sw", "Kiswahili" },
                    { "sw-CD", "Kiswahili (Congo [DRC])" },
                    { "sw-KE", "Kiswahili (Kenya)" },
                    { "sw-TZ", "Kiswahili (Tanzania)" },
                    { "sw-UG", "Kiswahili (Uganda)" },
                    { "syr", "Syriac" },
                    { "syr-SY", "Syriac (Syria)" },
                    { "ta-IN", "Tamil (India)" },
                    { "ta-LK", "Tamil (Sri Lanka)" },
                    { "ta-MY", "Tamil (Malaysia)" },
                    { "ta-SG", "Tamil (Singapore)" },
                    { "te-IN", "Telugu (India)" },
                    { "teo", "Teso" },
                    { "teo-KE", "Teso (Kenya)" },
                    { "teo-UG", "Teso (Uganda)" },
                    { "tg", "Tajik" },
                    { "tg-TJ", "Tajik (Tajikistan)" },
                    { "th-TH", "Thai (Thailand)" },
                    { "ti", "Tigrinya" },
                    { "ti-ER", "Tigrinya (Eritrea)" },
                    { "ti-ET", "Tigrinya (Ethiopia)" },
                    { "tig", "Tigre" },
                    { "tig-ER", "Tigre (Eritrea)" },
                    { "tk", "Turkmen" },
                    { "tk-TM", "Turkmen (Turkmenistan)" },
                    { "tn", "Setswana" },
                    { "tn-BW", "Setswana (Botswana)" },
                    { "tn-ZA", "Setswana (South Africa)" },
                    { "to", "Tongan" },
                    { "to-TO", "Tongan (Tonga)" },
                    { "tr-CY", "Turkish (Cyprus)" },
                    { "tr-TR", "Turkish (Türkiye)" },
                    { "ts", "Xitsonga" },
                    { "ts-ZA", "Xitsonga (South Africa)" },
                    { "tt", "Tatar" },
                    { "tt-RU", "Tatar (Russia)" },
                    { "twq", "Tasawaq" },
                    { "twq-NE", "Tasawaq (Niger)" },
                    { "tzm", "Central Atlas Tamazight" },
                    { "tzm-Arab", "Central Atlas Tamazight (Arabic)" },
                    { "tzm-Arab-MA", "Central Atlas Tamazight (Arabic, Morocco)" },
                    { "tzm-DZ", "Central Atlas Tamazight (Algeria)" },
                    { "tzm-MA", "Central Atlas Tamazight (Morocco)" },
                    { "tzm-Tfng", "Central Atlas Tamazight (Tifinagh)" },
                    { "tzm-Tfng-MA", "Central Atlas Tamazight (Tifinagh, Morocco)" },
                    { "ug", "Uyghur" },
                    { "ug-CN", "Uyghur (China)" },
                    { "uk-UA", "Ukrainian (Ukraine)" },
                    { "ur-IN", "Urdu (India)" },
                    { "ur-PK", "Urdu (Pakistan)" },
                    { "uz-Arab", "Uzbek (Arabic)" },
                    { "uz-Arab-AF", "Uzbek (Arabic, Afghanistan)" },
                    { "uz-Cyrl", "Uzbek (Cyrillic)" },
                    { "uz-Cyrl-UZ", "Uzbek (Cyrillic, Uzbekistan)" },
                    { "uz-Latn", "Uzbek (Latin)" },
                    { "uz-Latn-UZ", "Uzbek (Latin, Uzbekistan)" },
                    { "vai", "Vai" },
                    { "vai-Latn", "Vai (Latin)" },
                    { "vai-Latn-LR", "Vai (Latin, Liberia)" },
                    { "vai-Vaii", "Vai (Vai)" },
                    { "vai-Vaii-LR", "Vai (Vai, Liberia)" },
                    { "ve", "Venda" },
                    { "ve-ZA", "Venda (South Africa)" },
                    { "vi-VN", "Vietnamese (Vietnam)" },
                    { "vo", "Volapük" },
                    { "vo-001", "Volapük (world)" },
                    { "vun", "Vunjo" },
                    { "vun-TZ", "Vunjo (Tanzania)" },
                    { "wae", "Walser" },
                    { "wae-CH", "Walser (Switzerland)" },
                    { "wal", "Wolaytta" },
                    { "wal-ET", "Wolaytta (Ethiopia)" },
                    { "wo", "Wolof" },
                    { "wo-SN", "Wolof (Senegal)" },
                    { "xh", "isiXhosa" },
                    { "xh-ZA", "isiXhosa (South Africa)" },
                    { "xog", "Soga" },
                    { "xog-UG", "Soga (Uganda)" },
                    { "yav", "Yangben" },
                    { "yav-CM", "Yangben (Cameroon)" },
                    { "yi", "Yiddish" },
                    { "yi-001", "Yiddish (world)" },
                    { "yo-BJ", "Yoruba (Benin)" },
                    { "yo-NG", "Yoruba (Nigeria)" },
                    { "yrl", "Nheengatu" },
                    { "yrl-BR", "Nheengatu (Brazil)" },
                    { "yrl-CO", "Nheengatu (Colombia)" },
                    { "yrl-VE", "Nheengatu (Venezuela)" },
                    { "zgh", "Standard Moroccan Tamazight" },
                    { "zgh-MA", "Standard Moroccan Tamazight (Morocco)" },
                    { "zh", "Chinese" },
                    { "zh-Hans", "Chinese (Simplified)" },
                    { "zh-Hans-CN", "Chinese (Simplified, China)" },
                    { "zh-Hans-HK", "Chinese (Simplified, Hong Kong SAR)" },
                    { "zh-Hans-MO", "Chinese (Simplified, Macao SAR)" },
                    { "zh-Hans-SG", "Chinese (Simplified, Singapore)" },
                    { "zh-Hant", "Chinese (Traditional)" },
                    { "zh-Hant-HK", "Chinese (Traditional, Hong Kong SAR)" },
                    { "zh-Hant-MO", "Chinese (Traditional, Macao SAR)" },
                    { "zh-Hant-TW", "Chinese (Traditional, Taiwan)" },
                    { "zu", "isiZulu" },
                    { "zu-ZA", "isiZulu (South Africa)" }
                });
        }
    }
}

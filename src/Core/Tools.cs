using System;
using System.Linq;

namespace oauth2testbed.Core
{
    public class Tools
    {
        /// <summary>
        /// Instance of random.
        /// </summary>
        public static Random RandomInstance  = new Random();

        /// <summary>
        /// Generate a random string.
        /// </summary>
        /// <param name="length">Desired length of string.</param>
        /// <returns>Generated string.</returns>
        public static string GenerateRandomString(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            return new string(
                Enumerable.Repeat(chars, length)
                    .Select(n => n[RandomInstance.Next(n.Length)])
                    .ToArray());
        }

        /// <summary>
        /// Get a random name.
        /// </summary>
        /// <returns>Name.</returns>
        public static string GetRandomName()
        {
            var names = new[]
            {
                "scubaraspberry",
                "skeletonizersimply",
                "languidseed",
                "shellcomb",
                "awardforked",
                "limewellingtons",
                "unioncheesecake",
                "glandslowly",
                "giddypatter",
                "gatewaybuttondown",
                "optionbean",
                "keyboardpoppy",
                "showdisguising",
                "collarbonebars",
                "industrysubsequent",
                "gladcook",
                "pretzelsbullfinche",
                "tamesew",
                "cheesesteakpoached",
                "haggishay",
                "whoppingglance",
                "leafwidely",
                "excitementhyena",
                "jetsneering",
                "jumperplot",
                "sobmosquitoe",
                "restaurantbelieve",
                "flyingfishpen",
                "boxingcartload",
                "potbreasts",
                "enjoinmissile",
                "pantgarage",
                "widowraid",
                "nikesinge",
                "ultracrack",
                "richessefirst",
                "bedbalaclava",
                "goshawkattach",
                "huhovercoat",
                "wakefulwoof",
                "speciesludicrous",
                "flingintrusion",
                "fuzzyguatemalan",
                "politicsgnaw",
                "thussupposed",
                "podcompletely",
                "fictionnoun",
                "impetuoussiege",
                "clamsbarberry",
                "washboardjoke",
                "treeauction",
                "arrayhysterical",
                "oryxtype",
                "bacteriaverdant",
                "burgeestaysail",
                "heataboriginal",
                "growlingmembrane",
                "marchmob",
                "glaringgillette",
                "bladevisual",
                "outcomeport",
                "dreamshocked",
                "mothphone",
                "medicinepuzzled",
                "enigmaticgallery",
                "actorrowdy",
                "literatedesperate",
                "homerunswindler",
                "billiardshowl",
                "barkratline",
                "wrapseeds",
                "handletripwire",
                "impgripping",
                "cameramanfailure",
                "pineapplestill",
                "gulljournal",
                "thiefwiry",
                "formationlava",
                "exhaustedrelax",
                "topicstart",
                "coherentfuzzy",
                "strategyearn",
                "amplepleasing",
                "braziliansilky",
                "populationpromise",
                "somewheretricky",
                "marketspecies",
                "poethissing",
                "emperordamage",
                "chamoiskit",
                "burnishjockstrap",
                "bowlinepodiatrist",
                "assistancewing",
                "movementreality",
                "scornfulmuffin",
                "knickerstooth",
                "safedeter",
                "deputyburdensome",
                "argentgrotesque",
                "notebookwith",
                "splitbilliards",
                "dadstemson",
                "chieftheater",
                "seniorsentence",
                "kneelfew",
                "educationsport",
                "professorgenuine",
                "strengthlocation",
                "peonyfamily",
                "giftedteenager",
                "frapstrongly",
                "aromaticraise",
                "benefitdisgusting",
                "slacksbreath",
                "lizardsteadfast",
                "decisionmoral",
                "meanaerobics",
                "sellchildhood",
                "cookcheeseburger",
                "nigerianchemical",
                "butterflylap",
                "presentlame",
                "parrelbrisk",
                "greyinboard",
                "deafeningdetailed"
            };

            return names[RandomInstance.Next(names.Length)];
        }
    }
}
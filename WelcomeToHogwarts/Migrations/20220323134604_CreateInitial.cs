using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WelcomeToHogwarts.Migrations
{
    public partial class CreateInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ingredient",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredient", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Room",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Room", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HouseType = table.Column<string>(type: "varchar(50)", nullable: false),
                    PetType = table.Column<string>(type: "varchar(50)", nullable: false),
                    RoomId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Student_Room_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Room",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Recipe",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MakerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipe", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recipe_Student_MakerId",
                        column: x => x.MakerId,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IngredientRecipe",
                columns: table => new
                {
                    IngredientsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RecipesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientRecipe", x => new { x.IngredientsId, x.RecipesId });
                    table.ForeignKey(
                        name: "FK_IngredientRecipe_Ingredient_IngredientsId",
                        column: x => x.IngredientsId,
                        principalTable: "Ingredient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IngredientRecipe_Recipe_RecipesId",
                        column: x => x.RecipesId,
                        principalTable: "Recipe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Potion",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MakerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BrewingStatus = table.Column<string>(type: "varchar(50)", nullable: false),
                    RecipeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Potion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Potion_Recipe_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Potion_Student_MakerId",
                        column: x => x.MakerId,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IngredientPotion",
                columns: table => new
                {
                    IngredientsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PotionsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientPotion", x => new { x.IngredientsId, x.PotionsId });
                    table.ForeignKey(
                        name: "FK_IngredientPotion_Ingredient_IngredientsId",
                        column: x => x.IngredientsId,
                        principalTable: "Ingredient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IngredientPotion_Potion_PotionsId",
                        column: x => x.PotionsId,
                        principalTable: "Potion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Ingredient",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("000d2b41-8e42-4192-83bb-7b93c4d2dc9e"), "Essence of Daisyroot" },
                    { new Guid("00769042-bed2-4989-8b06-5b4453155a10"), "Bundimun Secretion" },
                    { new Guid("0159773d-0a5c-4a3d-89f9-4b74d09723f9"), "Syrup of Arnica" },
                    { new Guid("01b3f9fe-781b-499d-8256-25d7076e45fb"), "Iguana blood" },
                    { new Guid("023e0b09-c375-4c97-b031-75942122d05f"), "Essence of comfrey" },
                    { new Guid("03796871-a100-4879-9cbc-7351e639de18"), "Syrup of Hellebore" },
                    { new Guid("04cf4b79-ee67-47ba-905a-765b75427751"), "Cockroach" },
                    { new Guid("07e4095f-e415-4195-bc92-f1ebe36061a5"), "Lady's Mantle" },
                    { new Guid("085a78bc-fd14-423a-8670-f268d9cd4332"), "Wormwood" },
                    { new Guid("0a3c6ef4-ce20-4cc6-93ca-e80bc9d5fdb2"), "Acromantula venom" },
                    { new Guid("0c4020b4-c6a0-4446-a687-4395d47215d3"), "Graphorn horn" },
                    { new Guid("0c671cdd-ff65-4fd6-9f1d-7a2ce1a8330b"), "Puffer" },
                    { new Guid("0cf2a2ed-e953-4009-9e07-82bbd10d69a6"), "Flower head" },
                    { new Guid("0d0e02a1-8e6a-4db0-8c0f-ad387e36cc1f"), "St John's" },
                    { new Guid("0d60d939-6a26-42fc-96e0-5fd41a783e6c"), "African Red Pepper" },
                    { new Guid("0ef3ac9b-dd9b-4f55-b438-00ccf623b856"), "Ginger Root" },
                    { new Guid("0fdbc57c-d9d2-42f9-96d1-121e7aad5362"), "Bursting mushroom" },
                    { new Guid("1078fc17-ad9d-4e3b-a1bd-41159bd99902"), "Asian Dragon Hair" },
                    { new Guid("11451bcf-3b44-4772-a870-8e720ba5ebf2"), "Nagini's venom" },
                    { new Guid("116beb1f-268d-4742-a448-a3b833b4c253"), "Bloodroot" },
                    { new Guid("1207da70-ee77-46d0-aa17-526cfaea9a9b"), "Horseradish" },
                    { new Guid("12e36217-e098-4c8e-b900-cf0698844a63"), "Sneezewort" },
                    { new Guid("14710dfb-6451-441e-8a62-4ed8a120e1da"), "Venomous Tentacula" },
                    { new Guid("153abd74-2ff8-4904-9817-92508c8a06dd"), "Unicorn Hair" },
                    { new Guid("1657730e-4a62-44dc-8287-93274af268b6"), "Nux Myristica" },
                    { new Guid("168acb42-1f56-4a7f-bd03-c4cde0868bb1"), "Bicorn Horn" },
                    { new Guid("1adc2bd4-1aee-45e9-8931-e60a85d9c87e"), "Dandelion root" },
                    { new Guid("1b64d760-5103-4948-9cc5-c36562740f59"), "Bouncing Bulb" },
                    { new Guid("1bd3864d-59a9-4c1a-a2cd-e5e751163b79"), "Porcupine quill" },
                    { new Guid("1e06d2fd-44b3-43e0-aba6-74defff34f11"), "Hemlock" },
                    { new Guid("1e9e2c16-ac4e-40fc-a687-5cd9495ab45e"), "Murtlap tentacle" },
                    { new Guid("1f14c2d4-5a27-453d-adb8-c3eec901bd1b"), "Giant Purple Toad Wart" },
                    { new Guid("21637063-8ce5-4296-a146-84c1fff8eed0"), "Thyme" },
                    { new Guid("22638359-b80b-4549-a22a-01e89fb5f4dd"), "Moly" },
                    { new Guid("264caf35-dccb-4039-88d7-8ae0b4e1f1a6"), "Deadlyius" },
                    { new Guid("269106a2-ea43-4b6d-8104-3b03325c6bae"), "Hellebore" },
                    { new Guid("28cc9200-5be9-4b0d-b401-caf3b721a920"), "Petroleum Jelly" },
                    { new Guid("291f622d-2511-4f82-98ca-ac58cc5b39b2"), "Star Grass" },
                    { new Guid("2b8c7070-f22e-427c-9bec-0fbf54d743d2"), "Caterpillar" },
                    { new Guid("2baa8db3-7b91-4a61-bb16-e3b8a717ffa6"), "Spleenwart" },
                    { new Guid("2c679409-78e2-47be-8c3c-8f2b5536b336"), "Ashwinder egg" },
                    { new Guid("2ca82e56-f491-4afd-9fd8-b08b86e1a5c4"), "Wiggenbush" }
                });

            migrationBuilder.InsertData(
                table: "Ingredient",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("2cfe3bda-adcd-49b2-8241-31810c79fb63"), "Flesh" },
                    { new Guid("2dea2df5-f080-4e9c-9714-36f77160bafb"), "Anjelica" },
                    { new Guid("2e430a0d-cc24-4a13-b050-d439861aa808"), "Moonstone" },
                    { new Guid("2f0b2485-6b09-46b0-b6a8-5ff482d3de99"), "Camphirated Spirit" },
                    { new Guid("3027334c-45c7-46fe-8a84-a6c98312ff19"), "Squill bulb" },
                    { new Guid("30eee669-4d50-4721-b876-d5a81a8fcfd7"), "Flying Seahorses" },
                    { new Guid("35238d70-196f-4092-8f80-87a18821adbd"), "Thaumatagoria" },
                    { new Guid("353661a8-85c4-473b-a7dd-428f487ddbdc"), "Venomous Tentacula leaf" },
                    { new Guid("35489e05-483f-48ff-82bb-21472bfd1208"), "Puffskein hair" },
                    { new Guid("3578c1b0-73f4-4531-9a2f-ab8f823c46ba"), "Baneberry" },
                    { new Guid("359a6f77-ebe0-497c-b797-17dc7440e75f"), "Fire" },
                    { new Guid("35b3f732-deb3-4e2f-b9ac-c7dc81329bb4"), "Howlet's Wing" },
                    { new Guid("377e23a3-f852-4d3b-a2e0-44adc5458085"), "Dragonfly thorax" },
                    { new Guid("3a0e3d9d-f133-4155-8d7c-2972dab39e7e"), "Scurvy grass" },
                    { new Guid("3cf92033-e0d7-4c60-99c8-e5f141401b7f"), "Dragon blood" },
                    { new Guid("3f4e0e99-d6c1-461a-830e-987e86f4f818"), "Starthistle" },
                    { new Guid("3f51978b-7b49-4999-96de-05ffea4dd0d6"), "African Sea Salt" },
                    { new Guid("3fea520c-ab6f-48f6-aeb8-ae9322b05be0"), "Mandrake, stewed" },
                    { new Guid("403001ed-261c-4a1b-8440-8feb3179f124"), "Standard Ingredient" },
                    { new Guid("40e613be-9a4d-410a-95ee-5fad0f654de5"), "Betony" },
                    { new Guid("40ede544-5065-47a9-8da9-7d5ab5635bb3"), "Plangentine" },
                    { new Guid("40fba3eb-43f4-4d7d-a78d-d50b7438bb7f"), "Agrippa" },
                    { new Guid("414960ec-b325-4b77-a3ac-37476b02922a"), "Haliwinkles" },
                    { new Guid("41758923-2f85-4faa-8e52-464f75f5396f"), "Knarl quills" },
                    { new Guid("424d34fd-fe98-4e3b-a7fe-dc96720f7ead"), "Moonseed" },
                    { new Guid("43f069de-8276-471b-8ee7-10ac34e54287"), "Rose Petals" },
                    { new Guid("44aeb994-8e4b-426a-bad4-5db3a3b47827"), "Urine" },
                    { new Guid("454033ff-d1ce-45f8-95bb-0962aa3cec86"), "Bitter root" },
                    { new Guid("45672b27-2744-4efd-aa04-69daf0d0fac1"), "Tar" },
                    { new Guid("46b52b6d-eb1f-454f-8946-0e5f0bb9e2ad"), "Pomegranate juice" },
                    { new Guid("4a977d20-1f81-47d1-a634-d62661cd7b7a"), "Frog" },
                    { new Guid("4ab3d53f-f506-44bb-aced-2b5544c4d0cf"), "Horned toad" },
                    { new Guid("4c6cc841-c996-450f-b174-d94380f80619"), "Snakeweed" },
                    { new Guid("4cc4bc0c-fe89-42cc-af71-6538986164a1"), "Pus" },
                    { new Guid("4cf4cc72-0457-4e58-b986-83ce309d18e0"), "Motherwort" },
                    { new Guid("4d3f37a3-9b3e-43e1-83ff-2f742eedfad6"), "Scale of Dragon" },
                    { new Guid("508088e8-50b0-418f-bba6-1eeeedbac3e2"), "Bone" },
                    { new Guid("50885a63-522e-49cc-9b75-e9d0388f1dc8"), "Erumpent tail" },
                    { new Guid("536849df-04c7-489c-b4a2-a2e7b7f33ea6"), "Toe of Frog" },
                    { new Guid("569f0b0d-7ea1-4075-bc60-e35a90b25b27"), "Horklump juice" },
                    { new Guid("58272940-c04d-47e7-a292-e13b5cd731f3"), "Gomas Barbadensis" },
                    { new Guid("587514dc-7d18-45c3-9022-e3450083ed7c"), "Dragon dung" }
                });

            migrationBuilder.InsertData(
                table: "Ingredient",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("58dd0a37-ffe8-41b6-84ba-e46d4dd7604e"), "Boomslang" },
                    { new Guid("59e1f79d-a601-491b-bc3d-88b1fd49375d"), "Vinegar" },
                    { new Guid("5a04de3e-48f0-4ad5-a1dd-e891a16d301f"), "Kneazle hair" },
                    { new Guid("5a5c2917-b932-4e69-a286-eb1080b83ac4"), "Valerian root" },
                    { new Guid("5a709f99-30c9-4a99-b08d-08b02d71aad5"), "Shrivelfig" },
                    { new Guid("5aa884e0-291d-4e69-9999-d15bbfdc89bd"), "Butterscotch" },
                    { new Guid("5ac4fb82-4e54-414f-8958-1ac4387aa5f3"), "Spiders" },
                    { new Guid("5b9a51bc-44cc-432d-ae30-237a8d46bef4"), "Wiggentree bark" },
                    { new Guid("5c151cab-2f40-49aa-a3f9-82fc186cf381"), "Tormentil" },
                    { new Guid("5cb5d26b-232b-47b5-8df4-a2596d0be803"), "Lethe River Water" },
                    { new Guid("5e351030-728f-4aeb-bd3c-4fbf80404e29"), "Rose oil" },
                    { new Guid("5e3a93d6-f436-4792-a4eb-68e6873f8398"), "Blind" },
                    { new Guid("5fbdcec9-98a4-48c4-a8f4-3e40b6960b2c"), "Avocado" },
                    { new Guid("607a7f85-c2e4-49be-8f42-9def7f9f47ff"), "Neem oil" },
                    { new Guid("616cf142-8296-4abe-a1cd-1788de93081b"), "Blood" },
                    { new Guid("61c89bda-5eb3-4131-937d-58db6b53d761"), "Staghorn" },
                    { new Guid("61f37ba5-22e5-4265-a015-c9323301dc21"), "Frog brain" },
                    { new Guid("62106050-71d5-4f18-9582-2c5db10dea33"), "Leech" },
                    { new Guid("6299ddb1-c394-4e79-9ad4-02c9bb2f08cc"), "Exploding Ginger Eyelash" },
                    { new Guid("634f3c2d-5d85-4e14-bac5-4cc6b605d06d"), "Sloth brain" },
                    { new Guid("636b3e69-27c7-4e13-88af-e0ebed2f318f"), "Onion juice" },
                    { new Guid("6435ad32-f8a5-49d9-ac87-b5cf5fcb5e91"), "Silver" },
                    { new Guid("6462f6cc-ee03-4639-9e85-dcedecf64711"), "Pearl Dust" },
                    { new Guid("64eb4755-f206-4297-97ed-cfda34d06602"), "Mandrake Root" },
                    { new Guid("65bd9ba6-4a8a-4089-96ad-fdbf0107db81"), "Left handed nazle powder" },
                    { new Guid("662f1014-e0b4-4f9e-8c78-e8df9465a595"), "Flabberghasted Leech" },
                    { new Guid("682ec7df-49f1-48fc-86cd-ad10ae9009f5"), "Eel eye" },
                    { new Guid("6880cb76-f5e5-4d45-a915-050208014c50"), "Alcohol" },
                    { new Guid("68d8c797-2ae8-434b-9f4d-77e5bff401fb"), "Armadillo bile" },
                    { new Guid("68e53361-b82e-42c3-8cca-639d5358b23e"), "Lizard's Leg" },
                    { new Guid("69f19ba3-8863-48f0-b0c2-a4b2f44059c3"), "Maw" },
                    { new Guid("6ea122b9-618f-4309-812e-c1b931500c75"), "Wiggentree" },
                    { new Guid("6f5ce27b-d168-49f4-86dc-1800be4943a6"), "Cinnamon" },
                    { new Guid("6fa7219c-be8f-44f4-b754-cf2c04cede1f"), "Leech Juice" },
                    { new Guid("70774e7a-54f2-461d-a092-7ad553f77a93"), "Runespoor egg" },
                    { new Guid("71b5cb71-58f0-4359-9608-f1e15ec44a8d"), "Fanged Geranium" },
                    { new Guid("72c18e6a-0b5a-4e2d-908a-b70c06a1bf83"), "Gnat Heads" },
                    { new Guid("72f6f5fa-ab72-4f50-ab4c-326e8b62425e"), "Rat tail" },
                    { new Guid("7362f80e-94bc-49ca-b08b-13139915088c"), "Dandruff" },
                    { new Guid("74c2269d-38cf-4bcc-a864-ddcda7c739b0"), "Kelp" },
                    { new Guid("751d07e4-b062-4795-bde1-130a97a5227d"), "Eagle Owl Feather" },
                    { new Guid("751f33b2-5682-4b77-8fa0-14fb9d9aede8"), "Morning dew" }
                });

            migrationBuilder.InsertData(
                table: "Ingredient",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("75c73c01-27a6-4e54-a380-ab81034b8f35"), "Re'em blood" },
                    { new Guid("7698a144-0fe5-4a9c-8bd4-f261ec27e24c"), "Death" },
                    { new Guid("77fc3938-1160-4453-90cd-68ca77f1d615"), "Boom Berry" },
                    { new Guid("7860e74b-99ab-4930-beb1-27d3a6791396"), "Castor oil" },
                    { new Guid("7961a8d5-46d9-4ed5-91b5-ef089db7642a"), "Exploding Fluid" },
                    { new Guid("7cbc31b6-313f-448c-9fee-ca0672fae6ae"), "Honeywater" },
                    { new Guid("7d3ba179-0e22-40ad-9d3a-fd7508b1ae3f"), "Fairy Wing" },
                    { new Guid("7dc51a3d-2947-4c24-9ac4-6fb7426e0d10"), "Sopophorous bean" },
                    { new Guid("7e3ab76e-56f6-40b6-82d0-adb6a29cefbb"), "Arnica" },
                    { new Guid("7ec6ecbc-ef12-49eb-8961-bd0a7fb83eee"), "Gillyweed" },
                    { new Guid("7f08867d-e2aa-442d-b37c-792fb91d23c6"), "Sulphur Vive" },
                    { new Guid("7f5421a3-2534-41f6-9e8f-a8215aedccd9"), "Unicorn Horn" },
                    { new Guid("7f8c9ccd-e50f-4175-8989-1cb677c58c4b"), "Sage" },
                    { new Guid("7fa20ceb-1c4b-4564-8220-7ff03c03db37"), "Fillet of a Fenny Snake" },
                    { new Guid("803bd0fb-0ef6-4542-ae9f-93eb85ed20ae"), "Billywig Sting Slime" },
                    { new Guid("81539778-a05b-4813-8f96-ae9f33ea661c"), "Bat wing" },
                    { new Guid("82f5b42d-008a-459b-b73b-5c2650935f65"), "Banana" },
                    { new Guid("83bbc022-055b-4329-8043-ff94840d7fa5"), "Dragon horn" },
                    { new Guid("83fef774-0df2-4e7b-adab-a6f4c0ed8086"), "Angel's Trumpet" },
                    { new Guid("842cc10d-8ce3-4f98-a0bd-5c5fc0425bc1"), "Boomslang Skin" },
                    { new Guid("843f9937-37d1-4d63-a09a-89e6a3fc5c80"), "Salt water" },
                    { new Guid("84b3fdcb-1114-488e-96b5-99ce5429725e"), "White spirit" },
                    { new Guid("86919ed5-54b6-4467-b5e3-3207d1d79602"), "Salamander blood" },
                    { new Guid("8707ad30-5601-404c-a112-b8b4719ca2d0"), "Bezoar" },
                    { new Guid("872904db-3bf6-452f-8f1a-4ce62048e896"), "Liver" },
                    { new Guid("87b9a09b-ce0e-458b-af87-4ba2f533dbaa"), "Tubeworm" },
                    { new Guid("89babf3b-e0ee-4aec-a9ae-9678e4a5bd87"), "Mint" },
                    { new Guid("8a0ceee8-f099-4ab7-ab94-8a596317684a"), "Poison ivy" },
                    { new Guid("8a55b10e-a3d2-4105-9859-7c3e8ed65e48"), "Abraxan hair" },
                    { new Guid("8a5b7100-357b-42ad-bc72-80db2db4e805"), "Salpeter" },
                    { new Guid("8c1f89a0-e30e-49c7-a68a-22f4401ef754"), "Herbaria" },
                    { new Guid("8cdd8ad9-bce0-4a97-8423-31b8db105c90"), "Dittany" },
                    { new Guid("8d3613f8-2ae7-43fe-b4ed-190d62cea058"), "Rotten egg" },
                    { new Guid("8e4f0300-57d2-4b5f-b3c8-10e056e5fac0"), "Galanthus Nivalis" },
                    { new Guid("8eceee89-92d5-4b86-bde4-d8a87499a7a7"), "Lobalug Venom" },
                    { new Guid("908fd119-5d8f-4874-8240-cc24e12aeec7"), "Cheese" },
                    { new Guid("90cf9750-6761-44b6-9723-a317e9d78454"), "Horse hair" },
                    { new Guid("91984b37-b334-448a-a8c9-1fb6b0bbd336"), "Wormwood Essence" },
                    { new Guid("929dca60-4771-4350-9d3d-8111ef633a61"), "Vervain infusion" },
                    { new Guid("92ebdfc7-f15a-420b-a0a9-c5952cbb3a9c"), "Billywig sting" },
                    { new Guid("9360cabb-d5e1-49c1-92c5-635663eebdb6"), "Newt" },
                    { new Guid("94a91719-bb46-4a01-b79d-66ae4a3735d9"), "Goosegrass" }
                });

            migrationBuilder.InsertData(
                table: "Ingredient",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("9507c7f3-0417-4c1d-90aa-b83e6bc41ac9"), "Billywig wings" },
                    { new Guid("9521b734-42bf-424f-80ed-8bcb2faed536"), "White Wine" },
                    { new Guid("9579a951-cb7e-4e5e-91bc-319ff955aa6c"), "Unicorn Blood" },
                    { new Guid("962e07f2-b9a4-4c2f-ba50-dec0375c1e29"), "Chicken Lips" },
                    { new Guid("981169b8-203d-4d82-964c-5e05a95511d4"), "Mushroom" },
                    { new Guid("98bc63aa-fc6b-4520-9b2d-12ab393218ac"), "Pungous Onion" },
                    { new Guid("9ae58f6e-0522-46f1-a788-36925a01caa1"), "Lovage" },
                    { new Guid("9b591a9d-6e5a-4fa5-955e-53995c17a2f5"), "Dragon liver" },
                    { new Guid("9c17919a-fbc8-4c5b-a1ce-b8c75725c4a1"), "Foxglove" },
                    { new Guid("9cea68ad-4f62-4160-9079-b4aaabe22d50"), "Sal Ammoniac" },
                    { new Guid("9ee0d3e5-0841-4bc2-b570-0630895d52b4"), "Knotgrass" },
                    { new Guid("9fcc31d1-ac8b-4e02-bf58-6cd76846d053"), "Tooth of Wolf" },
                    { new Guid("a132c381-d10c-485c-ae9c-207a792b6234"), "Belladonna" },
                    { new Guid("a16a91bf-6f08-4035-80c9-315b7fa5026d"), "Lavender" },
                    { new Guid("a209777e-af5d-46ad-89da-bd64a29267f8"), "Pritcher's Porritch" },
                    { new Guid("a401135a-9f56-4c63-a19a-ea553edc31ce"), "Silverweed" },
                    { new Guid("a4db63fb-de40-45c9-a84a-b04350c51d75"), "Mallowsweet" },
                    { new Guid("a4ec5cb0-e6ed-46ac-9ecd-266927ad4449"), "Chizpurfle fang" },
                    { new Guid("a539c6c2-59e0-40ba-9d2b-2ee0c7450a8f"), "Rose thorn" },
                    { new Guid("a5dc5333-9806-4f1d-8308-05b06d8c2b82"), "Sopophorous plant" },
                    { new Guid("a5fd96d2-2c11-42fa-b8f6-c01b38ed28b5"), "Crocodile Heart" },
                    { new Guid("a647728d-c930-4dee-ace1-004cc0b6378b"), "Octopus Powder" },
                    { new Guid("a951b4a8-21e5-41f1-8b1a-996b506281e0"), "Bat spleen" },
                    { new Guid("a9a46c1a-33f9-49ff-872f-42b764371190"), "Lard" },
                    { new Guid("aa33b337-01cb-4cfa-a321-e8888f590392"), "Horned slug" },
                    { new Guid("ad623e55-1043-4c65-a1cc-2ddbbf2355dd"), "Mistletoe Berry" },
                    { new Guid("ad89459b-8410-4de6-90ee-2a5b7d0511a5"), "Mackled Malaclaw tail" },
                    { new Guid("adede490-fd35-407d-805f-5d98d0c36750"), "Daisy" },
                    { new Guid("ae48d92d-84d8-4a90-9aa5-6f7f69dc0919"), "Bulbadox juice" },
                    { new Guid("aeb88765-7af2-4486-92cb-97b541f0b691"), "Borage" },
                    { new Guid("b154ea1a-710c-447c-be5b-4eab59f0db36"), "Erumpent horn" },
                    { new Guid("b33abb83-57b2-4928-b6c4-a06f05e2e5fc"), "Griffin Claw" },
                    { new Guid("b3ec412d-fa88-423e-bfe1-c13f40666b1a"), "Lionfish" },
                    { new Guid("b4912d7b-13c8-4614-9db8-6d6b0c255db1"), "Wiggenbush bark" },
                    { new Guid("b49b7c2a-2c5a-4f09-a527-f433c209778a"), "Nightshade" },
                    { new Guid("b5a50a15-bb58-4436-816f-3719b2891e1c"), "Blowfly" },
                    { new Guid("b6481d38-cc77-4a05-9ad5-6160e810a51b"), "Flitterby" },
                    { new Guid("b6925515-2a50-4855-a44d-1ca66d1acd4d"), "Rat spleen" },
                    { new Guid("b6a1458e-04ee-452c-9783-da1d25bb0e22"), "Occamy egg" },
                    { new Guid("b6b19643-fe1a-4aea-9910-caa47140864c"), "Beetle Eye" },
                    { new Guid("b8c1e437-d200-4965-8cc3-8af7a79cf2bb"), "Gravy" },
                    { new Guid("b8f8032a-d478-4e21-8f4b-6fa391c5f66c"), "Aconite" }
                });

            migrationBuilder.InsertData(
                table: "Ingredient",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("ba1b185a-8276-4e92-91f8-54ddd5778894"), "Granian hair" },
                    { new Guid("bcb112e9-c600-439b-b6c2-98b0166e9090"), "Scarab beetle" },
                    { new Guid("bf2bfde0-6796-492b-828e-546aa4ad4ba9"), "Pickled Slugs" },
                    { new Guid("c0b13869-f067-40bb-beb2-89ed7e08a091"), "Salt" },
                    { new Guid("c1924ed3-65e3-46f0-b569-6b5782352a1b"), "Pond Slime" },
                    { new Guid("c1a26b75-8387-4535-8a44-032d61ac6aea"), "Witch's Ganglion" },
                    { new Guid("c1a472dd-1312-43b2-9626-620d445c25ed"), "Doxy egg" },
                    { new Guid("c380a7de-0de0-4ba0-8598-dcc509f3371f"), "Chinese Chomping Cabbage" },
                    { new Guid("c40226a1-ff8f-4113-8839-632d3abd96ed"), "Valerian" },
                    { new Guid("c58fe8cf-ed30-4c88-aa4c-5abe24ac2fb3"), "Lionfish Spine" },
                    { new Guid("c6fe4224-7512-496a-b964-420dcba47de2"), "Dragon claw" },
                    { new Guid("c70175a4-162c-4550-bbbd-3f76df092386"), "Rose" },
                    { new Guid("c88baafb-0328-407d-8d86-df5bfb2e97af"), "Tincture of Demiguise" },
                    { new Guid("ca1770e4-a55e-47dc-b7e6-d10894569ced"), "Flobberworm Mucus" },
                    { new Guid("ca5ea1cb-16b4-48d1-8441-a8cc5629b78a"), "Bubotuber pus" },
                    { new Guid("cda9eff4-0dc6-483b-b2e7-b551b3005fed"), "Jobberknoll feather" },
                    { new Guid("ce0c9361-37f8-450d-ba86-c8b4a94f48f9"), "Plantain" },
                    { new Guid("cf05738d-24ef-4a37-80da-377a42d1195e"), "Eye of Newt" },
                    { new Guid("cfacb4e4-c40e-42db-80ff-075187c99d5e"), "Mandrake" },
                    { new Guid("cfb0f48a-d176-4107-9cec-244d4f16549e"), "Rue" },
                    { new Guid("cfd617ef-ca8d-4287-bd49-615a63348e62"), "Ptolemy" },
                    { new Guid("d0226e74-4b1e-4758-8218-9a01d5d783c2"), "Alihotsy" },
                    { new Guid("d025cbe7-7586-4974-a4a7-4a728b236935"), "Niffler's Fancy" },
                    { new Guid("d0527b31-12a5-4a34-a9cb-96b72834889c"), "Antimony" },
                    { new Guid("d0a6d610-8427-4b34-ad70-3476b870f3e5"), "Eyeball" },
                    { new Guid("d1eb484b-122b-4323-861a-c0f97e8d269d"), "Newt spleen" },
                    { new Guid("d419cbe8-b5cb-45c5-bf20-4b19b4a6f22b"), "Dragon Claw Ooze" },
                    { new Guid("d4bab4d1-6b02-495c-85d7-c58c1472b6a0"), "Firefly" },
                    { new Guid("d637a308-aed3-4217-9ea9-079e6e985c65"), "Russian's Dragon Nails" },
                    { new Guid("d6e38cb2-c309-4fa2-a99a-95509aea8f4d"), "Vervain" },
                    { new Guid("d725d71d-2717-47cd-8754-c335ea6fae90"), "Infusion of Wormwood" },
                    { new Guid("d835a3df-3e77-41ab-aa00-87571531bff0"), "Tongue of Dog" },
                    { new Guid("d88401ce-8433-4f37-b12c-1e95a9031647"), "Cowbane" },
                    { new Guid("d982419e-3787-4b84-83ce-f92594d7b15c"), "Polypody" },
                    { new Guid("d9bb4f12-b1c1-447e-a797-bba9132cb35e"), "Gulf" },
                    { new Guid("d9bd6f3d-4ecf-4666-beb6-9d214bf13c3e"), "Jewelweed" },
                    { new Guid("db599abe-cca1-4bbc-918c-31486650d0f7"), "Water" },
                    { new Guid("db83ee9c-5ab6-4b61-966b-c01ed77ffca6"), "Woodlice Extract 63" },
                    { new Guid("dbb5afe5-9bcd-4be3-9f75-99525044275b"), "Mercury and Mars" },
                    { new Guid("deca364e-d816-454a-8695-9e2337a4f3d8"), "Nettle" },
                    { new Guid("df1ee644-95dd-47ca-be01-299e4c27d4dc"), "Wartcap powder" },
                    { new Guid("dfa63f49-efde-4a9f-bf4a-c9895d251e93"), "Powder of vipers" }
                });

            migrationBuilder.InsertData(
                table: "Ingredient",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("e1783541-d761-44e4-a3c1-e5e987b539a7"), "Squill" },
                    { new Guid("e29fc93d-1f2e-42bc-baaf-da7b10da54da"), "Honey" },
                    { new Guid("e32c323f-017b-46a4-9bef-5415bb3b2e9f"), "Gurdyroot" },
                    { new Guid("e639553f-d1f3-4494-998e-b69fd1bae85d"), "Witches' Mummy" },
                    { new Guid("e7178d41-021d-4169-a96f-169495c7a50e"), "Chizpurfle Carapace" },
                    { new Guid("e844d178-1787-4578-87e1-a3f0226a75a0"), "Wool of Bat" },
                    { new Guid("e8fb8749-6505-430b-949d-5cc73253bbeb"), "Adder's Fork" },
                    { new Guid("e943d3c6-e774-4652-9f98-c9431358cc34"), "Turtle Shell" },
                    { new Guid("e95fcabd-3f30-4b8d-816c-84b3217a9c6b"), "Moondew" },
                    { new Guid("e98b163d-78e2-420c-bc1e-268e9c847623"), "Saltpetre" },
                    { new Guid("ea865183-a299-44ce-b7b0-21fba0c3b104"), "Centaury" },
                    { new Guid("eb84a4da-e024-4b34-8c61-f3a7ddbeebd1"), "Bouncing Spider Juice" },
                    { new Guid("ebe4e96c-473b-46e0-9471-db7a5f3599a4"), "Sardine" },
                    { new Guid("ebe89d7e-97ba-43ae-93d2-5e51585417e1"), "Wood louse" },
                    { new Guid("ecdb81cf-647a-4c05-add8-5bb8aa4901be"), "Lacewing Fly" },
                    { new Guid("ed53d25c-b2b6-4d98-9bd5-967c119d3d9c"), "Puffer" },
                    { new Guid("f15eb62b-f49f-4197-b8e5-11109e0a6d90"), "Poppy head" },
                    { new Guid("f181e364-38f9-4152-a852-0b92847972d1"), "Cat Hair" },
                    { new Guid("f20f6fea-4598-4918-836e-3655fb148f0d"), "Fluxweed" },
                    { new Guid("f295a1b7-4f13-47d2-bd40-8ffd5a3c4674"), "Blatta Pulvereus" },
                    { new Guid("f3062bba-b77a-4e69-a5e4-30d201c8c984"), "Asphodel" },
                    { new Guid("f36faccb-8817-44d8-9e06-8c2342424fe5"), "Fire Seed" },
                    { new Guid("f44a5e0c-f52a-4ba2-b1b6-a0f4c66cfc86"), "Wartizome" },
                    { new Guid("f48d517a-de68-49ef-a849-b1c14dc31596"), "Armotentia" },
                    { new Guid("f65c6ec7-3ac8-49c9-8a93-bcc12471fe4e"), "Shrake spine" },
                    { new Guid("f91a747d-0c5b-4950-8f49-930b450239a5"), "Peacock feather" },
                    { new Guid("fa09940c-6a2d-4934-97ab-a7f9a83e25f5"), "Spirit of Myrrh" },
                    { new Guid("fa676d5d-5bec-4189-a02c-1e3e064ac939"), "Snake fang" },
                    { new Guid("fad19ffc-17e8-4fc9-ab99-98817eab1191"), "Corn starch" },
                    { new Guid("fb53b995-3e47-4e89-932b-5adf3f66587f"), "Peppermint" },
                    { new Guid("fb841c14-32d2-47ad-a1b5-d19fd93f7496"), "Streeler shells" },
                    { new Guid("fda2327d-95dd-4c9f-93e9-fe9668b0297a"), "Balm" },
                    { new Guid("fddcf602-9d24-480d-9cdb-a57145ae5cf8"), "Hermit crab shell" }
                });

            migrationBuilder.InsertData(
                table: "Room",
                columns: new[] { "Id", "Capacity", "Number" },
                values: new object[] { new Guid("c7d3b3f6-f8c1-4aa2-8363-131e08e09133"), 4, 101 });

            migrationBuilder.InsertData(
                table: "Student",
                columns: new[] { "Id", "HouseType", "Name", "PetType", "RoomId" },
                values: new object[,]
                {
                    { new Guid("f0d14492-c790-42e4-8452-9ea645b84800"), "Slytherin", "Draco Malfoy", "None", null },
                    { new Guid("f0d78492-c790-42e4-8452-9ea645b84800"), "Gryffindor", "Hermione Granger", "Cat", null }
                });

            migrationBuilder.InsertData(
                table: "Student",
                columns: new[] { "Id", "HouseType", "Name", "PetType", "RoomId" },
                values: new object[] { new Guid("c7d3b3f6-f8c1-4aa2-8363-721e08e09133"), "Gryffindor", "Harry Potter", "Owl", new Guid("c7d3b3f6-f8c1-4aa2-8363-131e08e09133") });

            migrationBuilder.CreateIndex(
                name: "IX_IngredientPotion_PotionsId",
                table: "IngredientPotion",
                column: "PotionsId");

            migrationBuilder.CreateIndex(
                name: "IX_IngredientRecipe_RecipesId",
                table: "IngredientRecipe",
                column: "RecipesId");

            migrationBuilder.CreateIndex(
                name: "IX_Potion_MakerId",
                table: "Potion",
                column: "MakerId");

            migrationBuilder.CreateIndex(
                name: "IX_Potion_RecipeId",
                table: "Potion",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipe_MakerId",
                table: "Recipe",
                column: "MakerId");

            migrationBuilder.CreateIndex(
                name: "IX_Student_RoomId",
                table: "Student",
                column: "RoomId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IngredientPotion");

            migrationBuilder.DropTable(
                name: "IngredientRecipe");

            migrationBuilder.DropTable(
                name: "Potion");

            migrationBuilder.DropTable(
                name: "Ingredient");

            migrationBuilder.DropTable(
                name: "Recipe");

            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropTable(
                name: "Room");
        }
    }
}

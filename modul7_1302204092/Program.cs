using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace modul7_1302204092
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BankTransferConfig configTransfer = new BankTransferConfig();

            Console.Write("Language (en/id): ");
            string pilBahasa = Console.ReadLine();

            if (pilBahasa == "en")
            {
                Console.WriteLine(configTransfer.config.lang.en);
            } else
            {
                Console.WriteLine(configTransfer.config.lang.id);
            }
        }
    }

    class BankTransferConfig
    {
        public TransferConfig config;
        public string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
        public string fileConfigName = "bank_transfer_config.json";

        public BankTransferConfig()
        {
            try
            {
                ReadConfigFile();
            }
            catch
            {
                WriteNewConfigFile();
                SetDefault();
            }
        }

        private TransferConfig ReadConfigFile()
        {
            string jsonStringFromFile = File.ReadAllText(path + "\\" + fileConfigName);
            config = System.Text.Json.JsonSerializer.Deserialize<TransferConfig>(jsonStringFromFile);
            return config;
        }

        private void SetDefault()
        {
            PesanLangConfig lang = new PesanLangConfig("Please insert the amount of money to transfer: ",
                "Masukkan jumlah uang yang akan di-transfer: ");
            NominalTransferConfig nominal = new NominalTransferConfig(25000000,6500,15000);
            List<string> methods = new List<string>() {"RTO (real-time)","SKN","RTGS","BI FAST"};
            PesanKonfirmasiConfig konfirm = new PesanKonfirmasiConfig("yes", "ya");
            config = new TransferConfig(lang, nominal, methods, konfirm);
        }

        private void WriteNewConfigFile()
        {
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                WriteIndented = true,
            };

            string jsonString = System.Text.Json.JsonSerializer.Serialize(config, options);
            File.WriteAllText(path + "\\" + fileConfigName, jsonString);
        }
    }

    class TransferConfig
    {
        public PesanLangConfig lang { get; set; }
        public NominalTransferConfig nominal { get; set; }
        public List<string> methods { get; set; }
        public PesanKonfirmasiConfig konfirma { get; set; }
    
        public TransferConfig() { }

        public TransferConfig(PesanLangConfig lang, NominalTransferConfig nominal, List<string> methods, PesanKonfirmasiConfig konfirm)
        {
            this.lang = lang;
            this.nominal = nominal;
            this.methods = methods;
            this.konfirma = konfirm;
        }
    }

    class PesanLangConfig
    {
        public string en { get; set; }
        public string id { get; set; }

        public PesanLangConfig() { }

        public PesanLangConfig(string en, string id)
        {
            this.en = en;
            this.id = id;
        }
    }

    class NominalTransferConfig
    {
        public int threshold { get; set; }
        public int low_fee { get; set; }
        public int high_fee { get; set; }

        public NominalTransferConfig() { }

        public NominalTransferConfig(int threshold, int low_fee, int high_fee)
        {
            this.threshold = threshold;
            this.low_fee = low_fee;
            this.high_fee = high_fee;
        }
    }

    class PesanKonfirmasiConfig
    {
        public string en { get; set; }
        public string id { get; set; }

        public PesanKonfirmasiConfig() { }

        public PesanKonfirmasiConfig(string en, string id)
        {
            this.en = en;
            this.id = id;
        }
    }


}

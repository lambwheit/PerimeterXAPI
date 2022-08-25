using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using PerimeterXAPI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



//app.UseHttpsRedirection();

static string Hash(string input)
{
    using (SHA1Managed sha1 = new SHA1Managed())
    {
        var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));
        var sb = new StringBuilder(hash.Length * 2);

        foreach (byte b in hash)
        {
            // can be "x2" if you want lowercase
            sb.Append(b.ToString("X2"));
        }

        return sb.ToString();
    }
}

app.MapGet("/", () =>
{
    return "hello world";

});
app.MapPost("/perimeterx/payload1", async delegate (HttpContext context)
{
    var remoteIpAddress = context.Connection.RemoteIpAddress;
    string jsonstring = "";
    using (StreamReader reader = new StreamReader(context.Request.Body, Encoding.UTF8))
    {
        jsonstring = await reader.ReadToEndAsync();
    }

    JObject o = JObject.Parse(jsonstring);
    Console.WriteLine(jsonstring);
    string appname, appversion, appPackagename, appid, pxver = "";
    try
    {
         appname = JToken.FromObject(o)["appname"].ToString(); ;
         appversion = JToken.FromObject(o)["appversion"].ToString();
         appPackagename = JToken.FromObject(o)["apppackagename"].ToString();
         appid = JToken.FromObject(o)["pxappid"].ToString();
         pxver = JToken.FromObject(o)["pxver"].ToString();

    }
    catch (Exception e)
    {
        return "Invalid Json Input";
    }
    Dictionary<string, string> BatteryDict = new Dictionary<string, string>()
    {
        { "charging", "AC" },
        { "discharging", "None" }
    };
    KeyValuePair<string,string> BatteryPair = BatteryDict.ElementAt(new Random().Next(0,2));
    string[] BatteryStatusLst = { "unknown", "good"};
    string BatteryStatus = BatteryStatusLst[new Random().Next(0, BatteryStatusLst.Length)];
    string BatteryPercent = (new Random().Next(1, 100)).ToString();
    string[] KernelVersionLst = { "4.9.270-g862f51bac900-ab7613625"};
    string KernelVer = KernelVersionLst[new Random().Next(0, KernelVersionLst.Length)];
    string[] rootedLst = { "true", "false" };
    string rooted = rootedLst[new Random().Next(0, rootedLst.Length)];
    string BatteryTemp = (new Random().Next(150, 320) * .1).ToString("F1");
    string BatteryVol = (new Random().Next(3700, 3800) * .001).ToString("F3");
    string UnixSeconds = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
    string[] Screensizes = { "1920,1080", "2560,1440", "1920,1032", "1280,800", "2560,1600" };
    string[] Screen = Screensizes[new Random().Next(0, Screensizes.Length)].Split(',');
    string screenheight = Screen[0];
    string screenwidth = Screen[1];
    string[] DeviceData = { "22,asus,Nexus 7", "23,asus,Nexus 7", "26,Asus,Nexus Player", "29,Google,Pixel", "27,Google,Pixel C", "29,Google,Pixel XL", "25,htc,Nexus 9", "27,Huawei,Nexus 6P", "22,LGE,Nexus 4", "23,LGE,Nexus 5", "27,LGE,Nexus 5X", "25,motorola,Nexus 6", "22,samsung,Nexus 10", "23,samsung,SCL23", "23,samsung,SCL24", "24,samsung,SAMSUNG-SM-G920AZ", "23,samsung,", "22,samsung,SAMSUNG-SM-T677A", "23,samsung,SC-01G", "23,samsung,SC-02G", "23,samsung,SC-03G", "23,samsung,SC-04F", "24,samsung,SC-04G", "24,samsung,SC-05G", "23,samsung,SM-A300FU", "23,samsung,SM-A300Y", "24,samsung,SM-A310F", "24,samsung,SM-A310M", "24,samsung,SM-A310N0", "24,samsung,SM-A310Y", "26,samsung,SM-A320F", "23,samsung,SM-A5000", "23,samsung,SM-A5009", "23,samsung,SM-A500F", "23,samsung,SM-A500F1", "23,samsung,SM-A500FU", "23,samsung,SM-A500G", "23,samsung,SM-A500H", "23,samsung,SM-A500K", "23,samsung,SM-A500L", "23,samsung,SM-A500M", "23,samsung,SM-A500S", "23,samsung,SM-A500W", "23,samsung,SM-A500Y", "25,samsung,SM-A5100", "23,samsung,SM-A5108", "24,samsung,SM-A510F", "24,samsung,SM-A510K", "24,samsung,SM-A510L", "24,samsung,SM-A510M", "24,samsung,SM-A510S", "24,samsung,SM-A510Y", "23,samsung,SM-A510Y", "26,samsung,SM-A520F", "26,samsung,SM-A520K", "26,samsung,SM-A520L", "26,samsung,SM-A520S", "26,samsung,SM-A520W", "23,samsung,SM-A7000", "23,samsung,SM-A7009", "23,samsung,SM-A700F", "23,samsung,SM-A700FD", "23,samsung,SM-A700H", "23,samsung,SM-A700K", "23,samsung,SM-A700L", "23,samsung,SM-A700S", "25,samsung,SM-A7100", "24,samsung,SM-A710F", "24,samsung,SM-A710K", "24,samsung,SM-A710L", "24,samsung,SM-A710M", "24,samsung,SM-A710S", "23,samsung,SM-A710Y", "26,samsung,SM-A720F", "23,samsung,SM-A8000", "23,samsung,SM-A800F", "23,samsung,SM-A800I", "23,samsung,SM-A800IZ", "24,samsung,SM-A800S", "23,samsung,SM-A800YZ", "26,samsung,SM-A810F", "26,samsung,SM-A810S", "23,samsung,SM-A9000", "26,samsung,SM-A9100", "26,samsung,SM-A910F", "26,samsung,SM-C5000", "26,samsung,SM-C5010", "26,samsung,SM-C7000", "26,samsung,SM-C9000", "26,samsung,SM-C900F", "22,samsung,SM-E500F", "22,samsung,SM-E500H", "22,samsung,SM-E500M", "22,samsung,SM-E700F", "22,samsung,SM-E700H", "22,samsung,SM-E700M", "22,samsung,SM-G150N0", "22,samsung,SM-G150NK", "22,samsung,SM-G150NL", "22,samsung,SM-G150NS", "22,samsung,SM-G155S", "22,samsung,SM-G360G", "22,samsung,SM-G360GY", "22,samsung,SM-G360T", "22,samsung,SM-G360T1", "22,samsung,SM-G361F", "22,samsung,SM-G361H", "22,samsung,SM-G361HU", "22,samsung,SM-G388F", "23,samsung,SM-G389F", "27,samsung,SM-G390F", "22,samsung,SM-G530P", "22,samsung,SM-G530R4", "22,samsung,SM-G530R7", "22,samsung,SM-G530T", "22,samsung,SM-G530T1", "22,samsung,SM-G530W", "22,samsung,SM-G531BT", "22,samsung,SM-G531F", "22,samsung,SM-G531H", "22,samsung,SM-G531M", "23,samsung,SM-G532F", "23,samsung,SM-G532M", "22,samsung,SM-G5500", "23,samsung,SM-G550FY", "25,samsung,SM-G550FY", "27,samsung,SM-G5520", "26,samsung,SM-G570F", "26,samsung,SM-G570M", "26,samsung,SM-G570Y", "22,samsung,SM-G6000", "23,samsung,SM-G600F", "23,samsung,SM-G600FY", "25,samsung,SM-G600S", "27,samsung,SM-G610F", "22,samsung,SM-G7202", "23,samsung,SM-G800F", "22,samsung,SM-G800H", "22,samsung,SM-G800M", "22,samsung,SM-G800R4", "23,samsung,SM-G800Y", "22,samsung,SM-G800Y", "23,samsung,SM-G860P", "22,samsung,SM-G870F0", "23,samsung,SM-G9006V", "23,samsung,SM-G9006W", "23,samsung,SM-G9008V", "23,samsung,SM-G9008W", "23,samsung,SM-G9009D", "23,samsung,SM-G9009W", "23,samsung,SM-G900F", "23,samsung,SM-G900FD", "23,samsung,SM-G900FQ", "23,samsung,SM-G900H", "23,samsung,SM-G900I", "23,samsung,SM-G900K", "23,samsung,SM-G900L", "23,samsung,SM-G900MD", "23,samsung,SM-G900P", "23,samsung,SM-G900R4", "23,samsung,SM-G900R6", "23,samsung,SM-G900R7", "23,samsung,SM-G900S", "23,samsung,SM-G900T", "23,samsung,SM-G900T1", "23,samsung,SM-G900T3", "23,samsung,SM-G900W8", "23,samsung,SM-G901F", "23,samsung,SM-G903F", "23,samsung,SM-G903M", "24,samsung,SM-G903W", "23,samsung,SM-G906K", "23,samsung,SM-G906L", "23,samsung,SM-G906S", "24,samsung,SM-G9200", "24,samsung,SM-G9208", "24,samsung,SM-G9209", "24,samsung,SM-G920F", "24,samsung,SM-G920I", "24,samsung,SM-G920K", "24,samsung,SM-G920L", "24,samsung,SM-G920P", "24,samsung,SM-G920R4", "24,samsung,SM-G920R7", "24,samsung,SM-G920S", "24,samsung,SM-G920T", "24,samsung,SM-G920T1", "24,samsung,SM-G920W8", "24,samsung,SM-G9250", "24,samsung,SM-G925F", "24,samsung,SM-G925I", "24,samsung,SM-G925K", "24,samsung,SM-G925L", "24,samsung,SM-G925P", "24,samsung,SM-G925R4", "24,samsung,SM-G925R7", "24,samsung,SM-G925S", "24,samsung,SM-G925T", "24,samsung,SM-G925W8", "24,samsung,SM-G9280", "24,samsung,SM-G9287", "24,samsung,SM-G9287C", "24,samsung,SM-G928C", "24,samsung,SM-G928F", "24,samsung,SM-G928G", "24,samsung,SM-G928I", "24,samsung,SM-G928K", "24,samsung,SM-G928L", "23,samsung,SM-G928N0", "24,samsung,SM-G928P", "24,samsung,SM-G928R4", "24,samsung,SM-G928S", "24,samsung,SM-G928T", "24,samsung,SM-G928W8", "26,samsung,", "26,samsung,SM-G930F", "26,samsung,SM-G930K", "26,samsung,SM-G930L", "26,samsung,SM-G930P", "26,samsung,SM-G930R4", "26,samsung,SM-G930S", "26,samsung,SM-G930T", "26,samsung,SM-G930U", "26,samsung,SM-G930W8", "24,samsung,SM-G9350", "26,samsung,SM-G9350", "26,samsung,SM-G935F", "26,samsung,SM-G935K", "26,samsung,SM-G935L", "26,samsung,SM-G935P", "26,samsung,SM-G935R4", "26,samsung,SM-G935S", "26,samsung,SM-G935T", "26,samsung,SM-G935U", "26,samsung,SM-G935W8", "26,samsung,SM-G950F", "26,samsung,SM-G950N", "26,samsung,SM-G950U", "24,samsung,SM-G950W", "26,samsung,SM-G955F", "26,samsung,SM-G955N", "26,samsung,SM-G955U", "24,samsung,SM-G955W", "22,samsung,SM-J105B", "22,samsung,SM-J105F", "22,samsung,SM-J105H", "22,samsung,SM-J105M", "22,samsung,SM-J105Y", "23,samsung,SM-J106B", "22,samsung,SM-J110M", "22,samsung,SM-J111F", "22,samsung,SM-J111M", "22,samsung,SM-J120F", "22,samsung,SM-J120FN", "22,samsung,SM-J120G", "22,samsung,SM-J120H", "22,samsung,SM-J120M", "23,samsung,SM-J120W", "22,samsung,SM-J120ZN", "22,samsung,SM-J200BT", "22,samsung,SM-J200F", "22,samsung,SM-J200G", "22,samsung,SM-J200GU", "22,samsung,SM-J200H", "22,samsung,SM-J200M", "22,samsung,SM-J200Y", "23,samsung,SM-J210F", "22,samsung,SM-J3109", "22,samsung,SM-J320F", "22,samsung,SM-J320FN", "22,samsung,SM-J320G", "22,samsung,SM-J320M", "23,samsung,SM-J320N0", "22,samsung,SM-J320P", "23,samsung,SM-J320R4", "22,samsung,SM-J320Y", "22,samsung,SM-J320YZ", "22,samsung,SM-J320ZN", "22,samsung,SM-J5007", "22,samsung,SM-J5008", "23,samsung,SM-J500F", "22,samsung,SM-J500F", "23,samsung,SM-J500FN", "23,samsung,SM-J500G", "23,samsung,SM-J500H", "23,samsung,SM-J500M", "22,samsung,SM-J500N0", "23,samsung,SM-J500Y", "25,samsung,SM-J510F", "25,samsung,SM-J510FN", "25,samsung,SM-J510GN", "25,samsung,SM-J510H", "25,samsung,SM-J510K", "25,samsung,SM-J510L", "25,samsung,SM-J510MN", "25,samsung,SM-J510S", "22,samsung,SM-J7008", "23,samsung,SM-J700F", "22,samsung,SM-J700F", "23,samsung,SM-J700H", "22,samsung,SM-J700K", "23,samsung,SM-J700M", "25,samsung,SM-J700P", "25,samsung,SM-J700T", "27,samsung,SM-J710F", "27,samsung,SM-J710GN", "23,samsung,SM-J710GN", "27,samsung,SM-J710K", "27,samsung,SM-J710MN", "22,samsung,SM-N750", "22,samsung,SM-N750K", "22,samsung,SM-N750L", "22,samsung,SM-N750S", "23,samsung,SM-N9100", "23,samsung,SM-N9106W", "23,samsung,SM-N9108V", "23,samsung,SM-N9109W", "23,samsung,SM-N910C", "23,samsung,SM-N910F", "23,samsung,SM-N910G", "23,samsung,SM-N910H", "23,samsung,SM-N910K", "23,samsung,SM-N910L", "23,samsung,SM-N910P", "23,samsung,SM-N910R4", "23,samsung,SM-N910S", "23,samsung,SM-N910T", "23,samsung,SM-N910T3", "23,samsung,SM-N910U", "23,samsung,SM-N910W8", "23,samsung,SM-N9150", "23,samsung,SM-N915F", "23,samsung,SM-N915FY", "23,samsung,SM-N915G", "23,samsung,SM-N915K", "23,samsung,SM-N915L", "23,samsung,SM-N915P", "23,samsung,SM-N915R4", "23,samsung,SM-N915S", "23,samsung,SM-N915T", "23,samsung,SM-N915W8", "23,samsung,SM-N916K", "23,samsung,SM-N916L", "23,samsung,SM-N916S", "24,samsung,SM-N9200", "24,samsung,SM-N9208", "24,samsung,SM-N920C", "24,samsung,SM-N920G", "24,samsung,SM-N920I", "24,samsung,SM-N920K", "24,samsung,SM-N920L", "24,samsung,SM-N920P", "24,samsung,SM-N920R4", "24,samsung,SM-N920R7", "24,samsung,SM-N920S", "24,samsung,SM-N920T", "24,samsung,SM-N920W8", "23,samsung,SM-N9300", "23,samsung,SM-N930F", "23,samsung,SM-N930K", "23,samsung,SM-N930L", "23,samsung,SM-N930S", "23,samsung,SM-N930W8", "23,samsung,SM-P350", "23,samsung,SM-P355", "23,samsung,SM-P355C", "23,samsung,SM-P355M", "23,samsung,SM-P355Y", "25,samsung,SM-P550", "23,samsung,SM-P550", "25,samsung,SM-P555", "23,samsung,SM-P555C", "25,samsung,SM-P555K", "25,samsung,SM-P555L", "25,samsung,SM-P555M", "25,samsung,SM-P555S", "27,samsung,SM-P580", "27,samsung,SM-P585N0", "22,samsung,SM-P600", "22,samsung,SM-P601", "22,samsung,SM-P605", "22,samsung,SM-T116IR", "22,samsung,SM-T235", "22,samsung,SM-T237P", "22,samsung,SM-T285", "22,samsung,SM-T285YD", "22,samsung,SM-T287", "22,samsung,SM-T330", "22,samsung,SM-T330NU", "22,samsung,SM-T331", "22,samsung,SM-T335", "22,samsung,SM-T335K", "22,samsung,SM-T335L", "22,samsung,SM-T337T", "25,samsung,SM-T350", "23,samsung,SM-T350", "25,samsung,SM-T355", "23,samsung,SM-T355C", "25,samsung,SM-T355Y", "22,samsung,SM-T365", "22,samsung,SM-T365F0", "22,samsung,SM-T365M", "22,samsung,SM-T365Y", "23,samsung,SM-T375L", "23,samsung,SM-T375S", "25,samsung,SM-T377P", "25,samsung,SM-T377R4", "22,samsung,SM-T533", "23,samsung,SM-T536", "25,samsung,SM-T550", "23,samsung,SM-T550", "25,samsung,SM-T555", "23,samsung,SM-T555C", "25,samsung,SM-T560NU", "27,samsung,SM-T580", "27,samsung,SM-T585", "27,samsung,SM-T587", "22,samsung,SM-T670", "22,samsung,SM-T677", "22,samsung,SM-T677NK", "22,samsung,SM-T677NL", "23,samsung,SM-T700", "23,samsung,SM-T705", "23,samsung,SM-T705C", "23,samsung,SM-T705M", "23,samsung,SM-T705Y", "24,samsung,SM-T710", "24,samsung,SM-T713", "23,samsung,SM-T713", "24,samsung,SM-T715", "24,samsung,SM-T715C", "24,samsung,SM-T715N0", "24,samsung,SM-T715Y", "24,samsung,SM-T719", "24,samsung,SM-T719C", "24,samsung,SM-T719Y", "23,samsung,SM-T800", "23,samsung,SM-T805", "23,samsung,SM-T805C", "23,samsung,SM-T805K", "23,samsung,SM-T805L", "23,samsung,SM-T805M", "23,samsung,SM-T805S", "23,samsung,SM-T805Y", "23,samsung,SM-T807", "23,samsung,SM-T807P", "23,samsung,SM-T807R4", "24,samsung,SM-T810", "24,samsung,SM-T813", "23,samsung,SM-T813", "24,samsung,SM-T815C", "24,samsung,SM-T815N0", "24,samsung,SM-T815Y", "24,samsung,SM-T817", "24,samsung,SM-T817P", "24,samsung,SM-T817T", "24,samsung,SM-T817W", "24,samsung,SM-T818", "24,samsung,SM-T818W", "24,samsung,SM-T819", "24,samsung,SM-T819C", "24,samsung,SM-T819Y", "26,samsung,SM-T820", "26,samsung,SM-T825", "22,samsung,SM-T900", "22,samsung,SM-T905", "23,samsung,SM-G900V", "24,samsung,SM-G920V", "24,samsung,SM-G925V", "24,samsung,SM-G928V", "26,samsung,SM-G930V", "26,samsung,SM-G935V", "22,samsung,SM-J100VPP", "23,samsung,SM-N910V", "23,samsung,SM-N915V", "24,samsung,SM-N920V", "22,samsung,SM-P605V", "22,samsung,SM-P905V", "22,samsung,SM-T337V", "25,samsung,SM-T377V", "23,samsung,SM-T707V", "22,samsung,SM-J320H", "22,samsung,SM-T280", "24,samsung,SAMSUNG-SM-G930AZ", "23,samsung,SM-G870W", "24,samsung,SM-G610Y", "24,samsung,SM-T817R4", "22,samsung,SM-G531Y", "23,samsung,SM-J510UN", "22,samsung,SM-T332", "27,samsung,SM-G610M", "24,samsung,SM-G925R6", "26,samsung,SM-G955U1", "27,samsung,SM-J727S", "23,samsung,SM-J106F", "22,samsung,SM-G360R6", "26,samsung,SM-G9500", "26,samsung,SM-G9550", "24,samsung,SM-N920R6", "26,samsung,SM-G930T1", "24,samsung,SM-G920R6", "23,samsung,SM-G5700", "23,samsung,SM-J327P", "26,samsung,SM-T825Y", "23,samsung,SM-G532MT", "26,samsung,SM-G9508", "27,samsung,SM-G615F", "22,samsung,SAMSUNG-SM-G530AZ", "23,samsung,SM-J106M", "23,samsung,SM-G532G", "24,samsung,SC-02J", "26,samsung,SM-T825C", "27,samsung,SM-P587", "24,samsung,SCV35", "27,samsung,SM-J530F", "24,samsung,SM-T818V", "26,samsung,SM-G950U1", "26,samsung,SM-A320FL", "26,samsung,SM-C701F", "26,samsung,SM-C5018", "26,samsung,SM-A810YZ", "26,samsung,SM-C7010", "24,samsung,SM-J327T", "24,samsung,SM-J327T1", "25,samsung,SM-J700T1", "27,samsung,SM-J727T", "27,samsung,SM-J727T1", "27,samsung,SM-J727V", "27,samsung,SM-T585N0", "22,samsung,SM-J3119", "23,samsung,SM-J5108", "23,samsung,SM-J106H", "25,samsung,SM-J320W8", "22,samsung,SM-J3110", "27,samsung,SM-P585Y", "27,samsung,SM-J530Y", "27,samsung,SM-J730FM", "27,samsung,SM-J730F", "26,samsung,SM-J330G", "24,samsung,SM-J327R6", "26,samsung,SM-C7018", "22,samsung,SM-J3119S", "27,samsung,SM-P588C", "23,samsung,SM-W2017", "27,samsung,SM-P585", "24,samsung,SM-T817V", "24,samsung,SM-T825N0", "24,samsung,SC-03J", "26,samsung,SM-G930R7", "27,samsung,SM-J701F", "27,samsung,SM-G615FU", "26,motorola,Moto Z (2)", "25,motorola,Moto Z (2)", "24,NVIDIA,SHIELD Android TV", "25,Amlogic,H96 PRO+", "25,Nextbit,Robin", "22,motorola,XT1058", "22,motorola,XT1053", "23,Fairphone,FP2", "23,Xiaomi,Redmi 3S", "24,samsung,SC-04J", "22,LENOVO,Lenovo A6020a46", "25,motorola,XT1635-02", "23,motorola,XT1580", "23,asus,msm8937 for arm64", "24,Xiaomi,Redmi Note 4", "26,samsung,SM-N950N", "24,motorola,Moto G (4)", "23,bq,Aquaris M10 FHD", "23,motorola,XT1254", "24,NVIDIA,SHIELD Tablet K1", "23,HUAWEI,HUAWEI WATCH", "22,LENOVO,Lenovo P1ma40", "26,Xiaomi,Mi A1", "23,samsung,SM-S327VL", "25,bq,Aquaris U Plus", "25,motorola,Moto G (5S) Plus", "25,motorola,XT1710-02", "24,motorola,Moto G (5)", "24,unknown,HUAWEI", "23,LGE,VS985 4G", "26,samsung,SM-N935S", "26,samsung,SM-N935K", "24,NVIDIA,SHIELD Tablet", "23,NVIDIA,SHIELD Tablet", "22,NVIDIA,SHIELD", "25,OnePlus,", "23,OnePlus,", "27,OnePlus,", "22,INTEX,CLOUD TREAD", "24,LENOVO,Lenovo P2a42", "23,BLU,BLU GRAND 5.5 HD", "27,motorola,Moto G (5) Plus", "23,TCL,5095K", "23,samsung,SM-G6100", "24,samsung,SAMSUNG-SM-N920A", "23,motorola,XT1609", "23,samsung,SM-S320VL", "23,archos,Archos 70 Oxygen", "25,ZTE,ZTE A2017G", "23,ZTE,ZTE A2017G", "26,samsung,SM-N950F", "22,motorola,XT1032", "23,asus,ASUS_Z00LD", "26,samsung,SM-J330F", "26,samsung,SM-N950U", "24,samsung,SM-P580", "22,alps,Nova_R1", "22,motorola,MotoE2(4G-LTE)", "24,asus,ASUS_Z012D", "27,samsung,SM-J327U", "23,LENOVO,Lenovo K10a40", "24,motorola,Moto C", "23,HTC,HTC One_M8", "27,samsung,SM-J727P", "26,OnePlus,", "24,samsung,SM-S337TL", "22,motorola,XT1021", "26,samsung,SM-A320Y", "23,samsung,SAMSUNG-SM-G900A", "27,samsung,SM-G5510", "23,Intex,Aqua Power M", "29,Google,Pixel 2", "29,Google,Pixel 2 XL", "24,unknown,msm8937_32", "23,PANASONIC,P55 Novo 4G", "25,motorola,moto x4", "26,samsung,SAMSUNG-SM-G891A", "23,alps,VEGA TAB-4G", "23,motorola,XT1097", "25,motorola,Moto G (5S)", "23,motorola,MotoG3", "25,samsung,SM-C7108", "26,samsung,SM-N9500", "25,Essential Products,PH-1", "27,samsung,SM-C7100", "27,samsung,SM-J530G", "27,samsung,SM-J710FQ", "27,samsung,SM-J730GM", "27,samsung,SM-J701M", "26,samsung,SM-N9508", "27,samsung,SM-J730G", "27,samsung,SM-J530GM", "27,samsung,SM-J701MT", "27,samsung,SM-J530YM", "26,samsung,SM-N935L", "25,samsung,SM-T385", "26,samsung,SM-N935F", "27,samsung,SM-C710F", "23,samsung,SM-G1650", "26,samsung,SM-J330FN", "25,samsung,SM-N950X", "27,samsung,SM-T385", "26,samsung,SM-N950U1", "26,samsung,SM-N950W", "26,samsung,SM-A720S", "26,samsung,SM-J330L", "27,samsung,SM-J3308", "27,samsung,SM-J727U", "27,samsung,SM-J327R7", "27,samsung,SM-J327R4", "23,alps,elink8735b_6tb_m", "23,ZOPO,ZP951", "23,k5s_pq551,Inco Plain2 S", "22,samsung,SM-N7505", "24,NVIDIA,SHIELD", "26,samsung,SM-A530F", "26,samsung,SM-A730F", "26,samsung,SM-C900Y", "26,samsung,SM-C9008", "26,samsung,SM-G892U", "25,samsung,SM-J250F", "25,samsung,SM-J250G", "27,samsung,SM-J3300", "27,samsung,SM-J530FM", "27,samsung,SM-J530K", "27,samsung,SM-J530L", "27,samsung,SM-J530S", "23,samsung,SM-J7108", "23,samsung,SM-J7109", "27,samsung,SM-J727R4", "27,samsung,SM-J730K", "27,samsung,SM-J327W", "26,samsung,SM-G935VC", "27,samsung,SM-P585M", "26,samsung,SM-G930VC", "27,samsung,SM-T380", "27,samsung,SM-T385M", "27,samsung,SM-T380C", "27,samsung,SM-T390", "24,samsung,SM-T818T", "27,samsung,SM-T395", "26,samsung,SM-T827R4", "27,samsung,SM-T395N", "27,samsung,SM-T385C", "27,samsung,SM-T585C", "27,samsung,SM-P583", "26,samsung,SM-T827V", "25,samsung,SM-J510FQ", "27,samsung,SM-G390W", "25,samsung,SM-T377W", "25,samsung,SM-T377T", "25,samsung,SM-A7108", "27,samsung,SM-T587P", "26,samsung,SM-T827", "23,samsung,SM-G1600", "23,samsung,SM-T378K", "23,samsung,SM-T378S", "23,samsung,SM-T378L", "27,samsung,SM-G390Y", "23,samsung,SM-G550T2", "23,samsung,SM-G550T1", "22,samsung,SM-T285M", "22,samsung,SM-T677V", "23,samsung,SM-N930V", "25,samsung,SM-J250M", "26,samsung,SM-A530N", "25,samsung,SM-G1650", "24,LGE,LG-M400", "27,samsung,SM-G610K", "27,samsung,SM-G610L", "27,samsung,SM-G610S", "26,samsung,SM-G611F", "27,samsung,SM-G611K", "27,samsung,SM-G611L", "26,samsung,SM-G611M", "27,samsung,SM-G611S", "22,samsung,SM-W2016", "29,Google,Pixel 3", "29,Google,Pixel 3 XL", "28,samsung,SM-A605GN", "29,samsung,SM-J810G", "28,samsung,SM-J810F", "28,samsung,SM-N960U", "27,samsung,SM-J260G", "28,samsung,SM-G9650", "28,samsung,SM-J810M", "28,samsung,SM-N960N", "28,samsung,SM-G965F", "28,samsung,SM-G965U", "28,samsung,SM-G960U", "28,samsung,SM-J810Y", "28,samsung,SM-A600FN", "28,samsung,SM-N960U1", "28,samsung,SM-G960F", "26,samsung,SM-J720F", "28,samsung,SM-N960F", "28,samsung,SM-G960U1", "28,samsung,SM-G965W", "29,samsung,SM-G965U1", "28,samsung,SM-G960W", "29,samsung,SM-G9600", "28,samsung,SM-J400M", "28,samsung,SM-A600N", "28,samsung,SM-A605FN", "28,samsung,SM-J400F", "28,samsung,SM-A605G", "28,samsung,SM-N960W", "28,samsung,SM-A600G", "28,samsung,SM-G9600", "28,samsung,SM-G960N", "26,samsung,SM-J720M", "28,samsung,SM-A605F", "27,samsung,SM-J260Y", "27,samsung,SM-T595C", "28,samsung,SM-G965N", "28,samsung,SM-A600F", "26,samsung,SM-J400F", "24,alps,KW88 Pro", "22,alps,S99A", "22,alps,KW99", "22,Masstel,N560", "24,LGE,LG-M320G", "24,alps,A02", "28,samsung,SM-J600F", "23,motorola,XT1663", "28,samsung,SM-J737V", "27,samsung,SM-J260M", "28,samsung,SM-J737T1", "28,samsung,SM-J737T", "27,samsung,SM-T837T", "27,motorola,moto z3", "24,A1,A1", "23,WIKO,U FEEL LITE", "27,BLU,Vivo XI+", "24,alps,KW68", "24,Infinix,Infinix X603", "24,GENPRO,S55A", "28,samsung,SM-J737U", "23,NUU,NUU_A3", "26,samsung,SM-J737A", "27,samsung,SM-S737TL", "24,CUBOT,CUBOT X18", "22,samsung,SC-01H", "24,BLU,Studio G3", "26,motorola,moto e5 plus", "23,samsung,SM-S727VL", "22,samsung,SM-G360V", "28,samsung,SM-T590", "27,samsung,SM-T590", "27,samsung,SM-T595", "27,samsung,SM-J260F", "27,vivo,vivo 1723", "24,ADVAN,5058", "22,alps,KW98", "22,alps,THOR PRO", "28,samsung,SM-J610FN", "28,samsung,SM-A750FN", "28,samsung,SM-A920F", "28,samsung,SM-G975F", "28,samsung,SM-J610F", "28,samsung,SM-J415F", "28,samsung,SM-G973F", "28,samsung,SM-J415FN", "28,samsung,SM-G970F", "28,samsung,SM-A505F", "28,samsung,SM-A750F", "27,samsung,SM-M205F", "25,samsung,SM-J320V", "28,samsung,SM-G970N", "28,samsung,SM-G975N", "24,Samsung Electronics Co., Ltd.,FiiO M6", "27,samsung,SM-M205G", "28,samsung,SM-J415G", "25,samsung,SAMSUNG-SM-J320A", "27,samsung,SM-M205FN", "28,samsung,SM-J600G", "28,samsung,SM-N9600", "28,samsung,SM-T835", "27,alps,FRT", "27,Xiaomi,Redmi 6A", "26,samsung,SM-G885F", "28,samsung,SM-T837", "28,samsung,SM-T830", "27,samsung,SM-J410F", "22,samsung,SM-T360", "28,samsung,SM-G970U", "28,samsung,SM-G973U", "26,samsung,SM-J737VPP", "28,samsung,SM-G973N", "28,samsung,SM-A605K", "28,samsung,SM-A6050", "28,samsung,SM-J600FN", "27,FIH,FIH-B2N-FIH", "28,samsung,SM-A405FN", "28,samsung,SM-T835N", "25,samsung,SM-J250Y", "22,samsung,SAMSUNG-SM-N915A", "25,asus,ASUS_Z01HD", "27,samsung,SM-T378V", "28,samsung,SM-T510", "24,KLIPAD,KLIPAD_KL600", "28,samsung,SM-A305F", "28,samsung,SM-A750GN", "29,Google,Pixel 3a", "29,Google,Pixel 3a XL", "28,motorola,moto g(6)", "26,samsung,SM-G8858", "26,samsung,SM-G8850", "28,samsung,SM-G8850", "22,BLU,LIFE X8", "27,samsung,SM-T597", "25,KYOCERA,S2720PP", "28,INFINIX MOBILITY LIMITED,Infinix X625B", "27,samsung,SM-J260T1", "28,Xiaomi,MI MAX 3", "27,PANASONIC,ELUGA X1", "25,KONKA,KONKA 610", "25,bq,Aquaris X5 Plus", "28,samsung,SM-S367VL", "24,samsung,SM-G5700", "25,asus,ASUS_Z01KD", "26,samsung,SM-J337W", "28,motorola,moto g(7) power", "28,samsung,SM-A205G", "27,nubia,NX611J", "23,Xiaomi,Redmi Note 4", "24,asus,ASUS_Z017D", "27,Xiaomi,Redmi 6", "23,BLU,BLU R1 HD", "23,BLU,R1 HD", "22,samsung,SAMSUNG-SM-G530A", "27,Alcatel,Alcatel_5059R", "26,samsung,SAMSUNG-SM-G930A", "24,Blackview,S8", "23,BLU,Tank Xtreme 5.0", "23,archos,Archos 101e Neon", "24,samsung,SAMSUNG-SM-J327AZ", "23,ZTE,Z956", "24,CUBOT,CUBOT KING KONG", "28,samsung,SM-A908B", "28,BLU,G9", "28,samsung,SM-J737P", "28,BLU,VIVO GO", "28,Sony,Kirin_dsds", "28,samsung,SM-T860", "28,samsung,SM-A505U1", "28,samsung,SM-J337A", "28,samsung,SM-A505W", "29,Google,Pixel 4", "29,Google,Pixel 4 XL", "27,samsung,SM-J260AZ", "28,samsung,SM-T725", "24,samsung,SAMSUNG-SM-J327A", "28,samsung,SM-A207F", "28,CUBOT,X20 PRO", "28,samsung,SM-A107F", "26,motorola,moto e5 play", "28,HTC,HTC U12+", "28,HTC,EXODUS 1", "26,samsung,SM-J337V", "24,samsung,SAMSUNG-SM-G920A", "26,samsung,SM-J337U", "27,LAVA,iris65", "28,Xiaomi,Redmi Note 8 Pro", "27,bq,Aquaris X Pro", "27,INFINIX MOBILITY LIMITED,Infinix X624B", "28,samsung,SM-A505U", "28,samsung,SM-J737R4", "26,samsung,SM-G885S", "28,motorola,moto g(7) play", "27,TINNO,V600AN", "30,samsung,SM-A405FN", "28,samsung,SM-A205U", "28,samsung,SM-S767VL", "29,samsung,SM-A715F", "29,samsung,SM-A705GM", "24,WE,L8", "25,bq,Aquaris U", "28,alps,ANT", "29,samsung,SM-A505G", "28,A-gold,F1", "30,samsung,SM-A505U", "28,samsung,SM-A205F", "30,samsung,SM-A515F", "23,samsung,SM-T807V", "28,samsung,SM-A105FN", "29,samsung,SM-G970F", "25,Teclast,T20(T2E2)", "28,samsung,SM-A102U", "29,samsung,SM-G9750", "29,samsung,SM-A305F", "23,samsung,SM-S120VL", "28,Razer,Phone 2", "27,Razer,Phone 2", "28,samsung,SM-M307F", "29,samsung,SM-M307F", "28,motorola,moto e6", "29,samsung,SM-G986U", "28,CUBOT,P30", "25,ZTE,K81", "29,samsung,SM-A505U1", "24,BLU,Advance A6", "25,Amlogic,Htv-6H", "27,ZTE,Z559DL", "28,ZTE,K83V", "24,ADVAN,i4U", "29,samsung,SM-M215F", "28,OnePlus,GM1917", "27,RED,H1A1000", "29,CUBOT,P40", "29,samsung,SM-G8870", "29,samsung,SM-A207M", "28,samsung,SM-A705W", "28,samsung,SM-T387V", "22,samsung,SM-P607T", "27,SPA Condor Electronics,Plume L3", "27,SPA Condor Electronics,Plume L2 Pro", "28,HMD Global,Nokia 2.3", "29,samsung,SM-T290", "24,samsung,SAMSUNG-SM-G928A", "22,HUAWEI,HUAWEI LUA-U22", "25,alps,G1", "29,motorola,moto g(7) power", "29,samsung,SM-A015T1", "29,samsung,SM-A013M", "28,ZTE,ZTE Blade L130", "29,ZTE,Z5157V", "29,samsung,SM-A115M", "29,samsung,SM-S215DL", "27,BLU,M6", "29,samsung,SM-A600T1", "29,samsung,SM-A215U", "23,ZTE,N9136", "22,samsung,SAMSUNG-SM-T337A", "22,LGE,LG-K130", "22,LGE,LG-K120", "29,OUKITEL,WP7", "29,asus,ASUS_I003DD", "29,samsung,SM-S102DL", "29,OUKITEL,C21", "27,BLU,C6 2019", "23,BLU,BLU STUDIO G2", "27,samsung,SM-A260G", "27,LGE,LMQ710MS", "29,HMD Global,Nokia C5 Endi", "29,samsung,SM-T307U", "29,samsung,SM-A217F", "29,samsung,SM-A107M", "30,samsung,SM-N981U1", "22,alps,Z Play", "30,samsung,SM-G973F", "28,Xiaomi,Redmi Note 7 Pro", "24,Samsung Electronics Co., Ltd.,FiiO M7", "22,Zebra Technologies,MC40N0", "23,samsung,SM-T230NW", "30,motorola,moto g stylus", "24,ADVAN,5060", "30,samsung,SM-F900F", "30,samsung,SM-A307FN", "29,samsung,SM-A107F", "29,samsung,SM-A105M", "29,samsung,SM-S111DL", "23,samsung,SAMSUNG-SM-N910A", "29,Xiaomi,lancelot", "29,motorola,moto g power (2021)", "25,samsung,SAMSUNG-SM-T377A", "30,samsung,SM-T295", "30,samsung,SM-A107F", "27,samsung,SM-J260GU", "30,samsung,SM-A102U1", "30,samsung,SM-G780F", "29,JOYAR,100011886", "30,samsung,SM-A326B", "30,samsung,SM-G988B", "23,samsung,SM-G9298", "30,samsung,SM-A528B", "23,samsung,SM-S907VL", "27,BLU,C5L", "29,samsung,SM-N986B", "30,LENOVO,Lenovo TB-7306F", "30,samsung,SM-S506DL", "30,HMD Global,Nokia C10", "30,samsung,SM-A305YN", "29,UMIDIGI,Power 3", "29,samsung,SM-A750F", "28,ZTE,Z3351S", "30,samsung,SM-M022G", "30,samsung,SM-S124DL", "30,motorola,moto g pure", "22,samsung,SAMSUNG-SM-T537A", "30,samsung,SM-A125U", "27,Ulefone,Power_5", "22,alps,MN4A2ZP/A", "30,samsung,SM-T220", "30,samsung,SM-F926U", "30,samsung,SM-G991W", "30,samsung,SM-A032F", "29,samsung,SM-T515", "30,samsung,SM-N986U1" };
    Console.WriteLine(DeviceData.Length);
    string[] selectedDevice = DeviceData[new Random().Next(0, DeviceData.Length)].Split(',');
    string androidVer = selectedDevice[0];
    string androdManufacturer = selectedDevice[1];
    string androidDevice = selectedDevice[2];
    Uuid uuid = new Uuid(PXRE.GenerateMostSignificantBytes(PXRE.ReturnUnixTime(PXRE.ScrewWithUnix1(DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()))),PXRE.GenerateLeastSignificantBytes());
string uuid1 = uuid.ToString();
string payload1 =
        "[{\"t\":\"PX315\",\"d\":{\"PX91\":"+screenwidth+",\"PX92\":"+screenheight+",\"PX316\":false,\"PX345\":1,\"PX351\":0,\"PX317\":\"wifi\",\"PX318\":\""+androidVer+"\",\"PX319\":\""+KernelVer+"\",\"PX320\":\""+androidDevice+"\",\"PX323\":"+UnixSeconds+",\"PX326\":\""+uuid1+"\",\"PX327\":\""+ uuid1.Split('-')[0] + "\",\"PX328\":\""+Hash(androidDevice+ uuid1 + uuid1.Split('-')[0]) +
        "\",\"PX337\":true,\"PX336\":true,\"PX335\":true,\"PX334\":false,\"PX333\":true,\"PX331\":true,\"PX332\":true,\"PX330\":\"new_session\",\"PX421\":\""+rooted+"\",\"PX442\":\"false\",\"PX339\":\""+androdManufacturer+"\",\"PX322\":\"Android\",\"PX340\":\"v"+ pxver + "\"," +
        "\"PX341\":\""+ appname + "\",\"PX342\":\""+ appversion + "\",\"PX348\":\""+appPackagename+"\"," +
        "\"PX343\":\"Unknown\",\"PX344\":\"missing_value\",\"PX347\":[\"en_US\"]," +
        "\"PX413\":\""+BatteryStatus+"\",\"PX414\":\"discharging\",\"PX415\":"+BatteryPercent+",\"PX416\":\"None\",\"PX419\":\"Li-ion\",\"PX418\":"+BatteryTemp+",\"PX420\":"+BatteryVol+"}}]";
    return "ftag=22&payload="+ Convert.ToBase64String(Encoding.UTF8.GetBytes(payload1))+ "&appId="+appid+"&tag=mobile&uuid=" + new Uuid(PXRE.GenerateMostSignificantBytes(PXRE.ReturnUnixTime(PXRE.ScrewWithUnix1(DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()))), PXRE.GenerateLeastSignificantBytes()).ToString();
});
//app.MapPost("/perimeterx/payload2", ([FromBody] string jsonstring) =>
app.MapPost("/perimeterx/payload2", async delegate (HttpContext context)
{

    string jsonstring = "";
    using (StreamReader reader = new StreamReader(context.Request.Body, Encoding.UTF8))
    {
        jsonstring = await reader.ReadToEndAsync();
    }
    Console.WriteLine(jsonstring);
    JObject o = JObject.Parse(jsonstring);
    
    string[] payload1array = {};
    string vid, sid, px349, PX320, challengeTimestamp, challengeSignature, PX257, PX339, PX340, PX341, PX342, PX348, PX413, PX415, PX418, PX420, PX343, PX344 = "";

    string payload1, payload2, challenge,pxappid, payload1Parse = "";
    try
    {
        //long startcount = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        payload1 = JToken.FromObject(o)["payload1"].ToString();
        challenge = JToken.FromObject(o)["challenge"].ToString();
        JObject challengeJObject = JObject.Parse(challenge);
        JArray challengeArray = JArray.Parse(challengeJObject["do"].ToString());
        vid = challengeArray[1].ToString().Split('|')[1];
        sid = challengeArray[0].ToString().Split('|')[1];
        challengeTimestamp = challengeArray[3].ToString().Split('|')[2];
        challengeSignature = challengeArray[3].ToString().Split('|')[3];
        payload1array = payload1.Split('&');
        pxappid = payload1array[2].Substring(6);
        payload1Parse = Encoding.UTF8.GetString(Convert.FromBase64String(payload1array[1].Substring(8)));
        Console.WriteLine(JArray.Parse(payload1Parse)[0].ToString());
        JObject payload1data = JObject.Parse(JToken.FromObject(JObject.Parse(JArray.Parse(payload1Parse)[0].ToString()))["d"].ToString());
        PX341 = JToken.FromObject(payload1data)["PX341"].ToString();
        PX342 = JToken.FromObject(payload1data)["PX342"].ToString();
        PX348 = JToken.FromObject(payload1data)["PX348"].ToString();
        PX340 = JToken.FromObject(payload1data)["PX340"].ToString();
        PX343 = JToken.FromObject(payload1data)["PX343"].ToString();
        PX344 = JToken.FromObject(payload1data)["PX344"].ToString();
        PX339 = JToken.FromObject(payload1data)["PX344"].ToString();
        PX420 = JToken.FromObject(payload1data)["PX420"].ToString();
        PX418 = JToken.FromObject(payload1data)["PX418"].ToString();
        PX415 = JToken.FromObject(payload1data)["PX415"].ToString();
        PX413 = JToken.FromObject(payload1data)["PX413"].ToString();
        PX320 = JToken.FromObject(payload1data)["PX320"].ToString();
        string[] Challenge = new List<string>(challengeArray[3].ToString().Split('|')).GetRange(4, 6).ToArray();
        PX257 = PXRE.ChallengeResponse(PX320, int.Parse(Challenge[2]), int.Parse(Challenge[3]), int.Parse(Challenge[0]), int.Parse(Challenge[4]), int.Parse(Challenge[1]), int.Parse(Challenge[5])).ToString();
        long endcount = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        px349 = (endcount - long.Parse(challengeTimestamp)).ToString();
        payload2 = "[{\"t\":\"PX329\",\"d\":{\"PX349\":"+ px349 + ",\"PX320\":\""+PX320+"\",\"PX259\":"+challengeTimestamp+",\"PX256\":\""+challengeSignature+"\",\"PX257\":\""+PX257+"\",\"PX339\":\""+PX339+"\",\"PX322\":\"Android\",\"PX340\":\""+PX340+"\",\"PX341\":\""+PX341+"\",\"PX342\":\""+PX342+"\",\"PX348\":\""+ PX348 + "\",\"PX343\":\""+PX343+"\",\"PX344\":\""+PX344+"\"," +
                   "\"PX347\":[\"en_US\"],\"PX413\":\""+PX413+"\",\"PX414\":\"discharging\",\"PX415\":"+PX415+"" +
                   ",\"PX416\":\"None\",\"PX419\":\"Li-ion\",\"PX418\":" +PX418 +",\"PX420\":"+PX420+"}}]";
    }
    catch (Exception e)
    {
        return "Invalid Json Input";
    }
    return "vid="+vid+ "&ftag=22&payload=" + Convert.ToBase64String(Encoding.UTF8.GetBytes(payload2))+"&appId=" +pxappid+"&tag=mobile&uuid=" + new Uuid(PXRE.GenerateMostSignificantBytes(PXRE.ReturnUnixTime(PXRE.ScrewWithUnix1(DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()))), PXRE.GenerateLeastSignificantBytes()).ToString() + "&sid="+sid;
});
app.Run();


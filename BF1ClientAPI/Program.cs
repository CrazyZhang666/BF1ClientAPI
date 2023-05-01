using BF1ClientAPI.SDK;
using BF1ClientAPI.Utils;

namespace BF1ClientAPI;

public class Program
{
    public const string Host = "http://127.0.0.1:10086";

    public static void Main(string[] args)
    {
        Console.Title = "ս��1�ͻ���API | DS By CrazyZhang666";

        if (!Memory.Initialize())
        {
            Console.WriteLine("ս��1�ͻ���API��ʼ��ʧ�ܣ�������ֹ");
            Console.WriteLine();

            Console.WriteLine("��������رմ˴���...");
            Console.ReadKey();

            return;
        }

        if (!Chat.AllocateMemory())
        {
            Console.WriteLine("�������������ڴ�ռ�ʧ�ܣ�������ֹ");
            Console.WriteLine();

            Console.WriteLine("��������رմ˴���...");
            Console.ReadKey();

            return;
        }

        Console.WriteLine("ս��1�ͻ���API��ʼ���ɹ���");
        Console.WriteLine();

        Console.WriteLine("----------------------------");
        Console.WriteLine($"����Id: \t{Memory.Bf1ProId}");
        Console.WriteLine($"���̾��: \t{Memory.Bf1ProHandle}");
        Console.WriteLine($"��ģ���ַ: \t0x{Memory.Bf1ProBaseAddress:X}");
        Console.WriteLine($"���ھ��: \t{Memory.Bf1WinHandle}");
        Console.WriteLine($"����ռ��ַ: \t0x{Chat.AllocateMemAddress:x}");
        Console.WriteLine("----------------------------");
        Console.WriteLine();

        Console.WriteLine($"�ӿ��ĵ���{Host}/index.html");
        Console.WriteLine("�� Ctrl+C ����������");
        Console.WriteLine();

        ////////////////////////////////////////////////////////

        CoreUtil.DisbleConsoleCloseButton(Console.Title);
        AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;

        ////////////////////////////////////////////////////////

        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddCors(cros =>
        {
            cros.AddPolicy("AllowAllOrigins", policy =>
            {
                policy.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
            });
        });

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("V1", new OpenApiInfo
            {
                Version = "V1",
                Title = "ս��1�ͻ���API",
                Description = "��ս��1�ͻ����ڴ��л�ȡ���ݣ������������Ե��ã���ҿɸ��ݴ�API�Զ��忪�����ܹ���",
                Contact = new OpenApiContact()
                {
                    Name = "GitHub��ַ",
                    Url = new Uri("https://github.com/CrazyZhang666/BF1ClientAPI")
                }
            });
            // ��ʾ�ĵ�ע��
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename), true);
        });

        var app = builder.Build();

        app.UseCors("AllowAllOrigins");

        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/V1/swagger.json", "API�汾 V1");
            options.RoutePrefix = string.Empty;
        });

        app.UseStaticFiles();
        app.UseAuthorization();
        app.MapControllers();

        app.Run(Host);
    }

    private static void CurrentDomain_ProcessExit(object sender, EventArgs e)
    {
        // �ͷ���������ָ���ڴ�
        Chat.FreeMemory();
        // �ͷ��ڴ�ģ�������Դ
        Memory.UnInitialize();
    }
}

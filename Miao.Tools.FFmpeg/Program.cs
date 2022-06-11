using System;
using System.IO;
using Miao.Core.Utils;
using Miao.Tools.FFmpeg.Enums;
using Miao.Tools.FFmpeg.Options;

namespace Miao.Tools.FFmpeg
{
    internal class Program
    {
        static readonly string FFmpeg = Path.Combine(Environment.CurrentDirectory, "Tools", "ffmpeg.exe");

        static void Main(string[] args)
        {
            //input file
            Console.WriteLine("please enter input file:");
            string inputFile = Console.ReadLine().Trim();
            if (string.IsNullOrEmpty(inputFile))
            {
                Console.WriteLine("input file is error.");
                return;
            }

            //choose transfer
            Console.WriteLine("please choose output format:");
            Console.WriteLine("1.mp4");
            Console.WriteLine("2.avi");
            Console.WriteLine("3.rmvb");
            string choice = Console.ReadLine().Trim();
            string outputFormat;
            if (choice == "1")
            {
                outputFormat = "mp4";
            }
            else if(choice == "2")
            {
                outputFormat = "avi";
            }
            else if(choice == "3")
            {
                outputFormat = "rmvb";
            }
            else
            {
                Console.WriteLine("your choice is error.");
                return;
            }

            //transfer execute
            string outputDir = Path.GetDirectoryName(inputFile);
            string outputFileName = $"{Path.GetFileNameWithoutExtension(inputFile)}_out.{outputFormat}";
            string outputFile = Path.Combine(outputDir, outputFileName);
            var ffmpegOptions = new FFmpegOptions(inputFile, outputFile)
                 .IsCover(true);
            ExecuteFFmpeg(ffmpegOptions.ToCommandArgs());

            Console.WriteLine("end.");
            Console.ReadKey();
        }

        static void VideoTransferTest()
        {
            var audioOptions = new AudioOptions()
                //.SetAudioCodecType(AudioCodecType.copy)
                .SetBitRate("64k")
                .SetChannels(AudioChannelsType.Two)
                .SetSamplingFrequency(44100);
            var videoOptions = new VideoOptions()
                .SetBitRate("200k")
                .SetRate(25)
                .SetSize(new VideoSize(640, 480))
                .SetVideoCodecType(VideoCodecType.libx264);
            var ffmpegOptions = new FFmpegOptions("Files\\input.mp4", "Files\\ouput.avi")
                .IsCover(true)
                .SetAudioOptions(audioOptions)
                .SetVideoOptions(videoOptions);
            ExecuteFFmpeg(ffmpegOptions.ToCommandArgs());
        }

        static void ExecuteFFmpeg(string commandArguments)
        {
            // ffmpeg.exe -i input.mp4 output.avi
            //string command = $"\"{FFmpeg}\" {commandArguments}";
            Console.WriteLine("ffmpeg arguments:" + commandArguments);
            var processUtil = new ProcessUtil(FFmpeg);
            processUtil.SetArguments(commandArguments);
            processUtil.AddOutDataReceivedHandler((sender, e) =>
            {
                Console.WriteLine(e.Data);
            });
            processUtil.AddErrorDataReceivedHandler((sender, e) =>
            {
                Console.WriteLine(e.Data);
            });

            bool processResult = processUtil.StartProcess();
            if (!processResult)
            {
                Console.WriteLine("process last error:" + processUtil.LastError);
            }
        }
    }
}

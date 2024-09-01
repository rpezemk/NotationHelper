namespace AudioTool
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var dirac1 = DspHelper.GetFlatResponse(100);
            AudioFileHelper.SaveAudio(dirac1, dirac1, 44100, "def1.wav");
        }
    }
}

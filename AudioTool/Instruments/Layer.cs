namespace AudioTool.Instruments
{
    public class Layer
    {
        public Layer(int no, string dyn, string instrName, string basePath, string extension, double sampleLen)
        {
            No = no;
            Dyn = dyn;
            InstrName = instrName;
            BasePath = basePath;
            Extension = extension;
            SampleLen = sampleLen;
        }

        public int No;
        public string Dyn;
        public string InstrName;
        public string BasePath;
        public string Extension;
        public double SampleLen;

        public string FilePath => $"{BasePath}{Dyn}-{InstrName}.{Extension}";
        public string ToWholeSample()
        {
            var str = @$"
                aLeft_{Dyn}_wav1, aRight_{Dyn}_wav1 diskin2 ""{FilePath}"", 1, p4
                aLeft_{Dyn} = aLeft_{Dyn}_wav1
                aRight_{Dyn} = aRight_{Dyn}_wav1
                ";
            return str;
        }

        public string ToFadeInOutSample()
        {
            var str = @$"
                aLeft_{Dyn}_wav1, aRight_{Dyn}_wav1 diskin2 ""{FilePath}"", 1, p4
                kEnv_{Dyn} linseg 0, {SampleLen}/2, 1, {SampleLen}/2, 0
                aLeft_{Dyn} = aLeft_{Dyn}_wav1 * kEnv_{Dyn}
                aRight_{Dyn} = aRight_{Dyn}_wav1 * kEnv_{Dyn}
                ";
            return str;
        }

        public string FadeZeroToOne()
        {
            var str = @$"
                aLeft_{Dyn}_wav1, aRight_{Dyn}_wav1 diskin2 ""{FilePath}"", 1, p4
                kEnv_{Dyn} linseg 0, {SampleLen}, 1
                aLeft_{Dyn} = aLeft_{Dyn}_wav1 * kEnv_{Dyn}
                aRight_{Dyn} = aRight_{Dyn}_wav1 * kEnv_{Dyn}
                ";
            return str;
        }

        public string FadeOneToZero()
        {
            var str = @$"
                aLeft_{Dyn}_wav1, aRight_{Dyn}_wav1 diskin2 ""{FilePath}"", 1, p4
                kEnv_{Dyn} linseg 1, {SampleLen}, 0
                aLeft_{Dyn} = aLeft_{Dyn}_wav1 * kEnv_{Dyn}
                aRight_{Dyn} = aRight_{Dyn}_wav1 * kEnv_{Dyn}
                ";
            return str;
        }



        /// <summary>
        /// kDiff_{layer.No}
        /// </summary>
        /// <param name="layer"></param>
        /// <param name="instrNo"></param>
        /// <param name="nLayers"></param>
        /// <returns></returns>
        public string ToCalculatedDynamics(int nLayers)
        {
            var res = @$"            
            kDiff_{Dyn} = kMod * {nLayers - 1} - {No}
            kRes_{Dyn} init 0
            if kDiff_{Dyn} > 1 || kDiff_{Dyn} < -1 then 
                kRes_{Dyn} = 0
            elseif kDiff_{Dyn} >= 0 then
                kRes_{Dyn} =   (1 - kDiff_{Dyn})
            elseif kDiff_{Dyn} < 0 then
                kRes_{Dyn} = - (1 - kDiff_{Dyn})
            endif
            ";
            return res;
        }
    }
}

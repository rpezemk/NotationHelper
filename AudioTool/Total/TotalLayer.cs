namespace AudioTool.Total
{
    public class TotalLayer
    {
        public TotalLayer(int no, string dyn, string instrName, string basePath, string extension, double sampleLen)
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
                ;BEGIN SECTION
                aLeft_{Dyn}_beginWave0, aRight_{Dyn}_beginWave0 diskin2 ""{FilePath}"", 1, iskiptim;
                aLeft_{Dyn}_beginWave1, aRight_{Dyn}_beginWave1 diskin2 ""{FilePath}"", 1, iskiptim;
                aLeft_{Dyn}_beginWave = 1.5 * (aLeft_{Dyn}_beginWave0 + aLeft_{Dyn}_beginWave1)
                aRight_{Dyn}_beginWave = 1.5 * (aRight_{Dyn}_beginWave0 + aRight_{Dyn}_beginWave1)
                
                ;END SECTION
                aLeft_{Dyn}_endWave  init 0
                aRight_{Dyn}_endWave init 0
                if kEndTrigger == 1 then
                    aLeft_{Dyn}_endWave, aRight_{Dyn}_endWave diskin2 ""{FilePath}"", 1, iskiptim + iEndSkip
                    aLeft_{Dyn}_endWave, aRight_{Dyn}_endWave diskin2 ""{FilePath}"", 1, iskiptim + iEndSkip
                endif
                
                ; PORTATO
                aLeft_{Dyn}  = aLeft_{Dyn}_beginWave  * kBegEnv
                aRight_{Dyn} = aRight_{Dyn}_beginWave * kBegEnv
                aLeft_{Dyn}  = aLeft_{Dyn}  + aLeft_{Dyn}_endWave  * kEndEnv;
                aRight_{Dyn} = aRight_{Dyn} + aRight_{Dyn}_endWave * kEndEnv;
                ";
            return str;
        }

        public string ToSimpleEnvelope()
        {
            var str = @$"
                ;;;;  iDuration  init p3
                ;;;;  iskiptim init p4
                ;;;;  iSampleLen init p5
                ;;;;  iBegLen min iDuration, iSampleLen
                ;;;;  iEndLen min iDuration, iSampleLen
                ;;;;  iPreEnd = max(0, iDuration - iEndLen)
                ;;;;  kBegEnv linseg 1, iBegLen, 0
                ;;;;  kEndEnv linseg 0, iPreEnd, 0, iEndLen, 1
                ;;;;  kEndTrigger init 0
                ;;;;  kFillEnv <---- this is envelope for fill layer
                ;;;;  FILL TRIGGERS
                ;;;;      kWaveTrig1 
                ;;;;      kWaveTrig2 
                ;BEGIN SECTION
                aLeft_{Dyn}_beginWave, aRight_{Dyn}_beginWave diskin2 ""{FilePath}"", 1, iskiptim ; + (4 * 12)
                

    
                aLeft_{Dyn}  = 1 * aLeft_{Dyn}_beginWave  * kFillEnv
                aRight_{Dyn} = 1 * aRight_{Dyn}_beginWave * kFillEnv
                
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
                kRes_{Dyn} =   1 - kDiff_{Dyn}
            elseif kDiff_{Dyn} < 0 then
                kRes_{Dyn} =  1 + kDiff_{Dyn}
            endif
            ";
            return res;
        }
    }
}

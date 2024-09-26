using AudioTool.Instruments.InstrumentBase;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioTool.CSoundWrapper
{

    public static class CsdGenerator
    {
        public static string GetScriptedInstruments(List<AScriptedInstrument> scriptedInstruments)
        {
            StringBuilder instrBuilder = new StringBuilder();
            StringBuilder eventsBuilder = new StringBuilder();
            var instrCounter = 100;
            foreach(var instr in scriptedInstruments)
            {
                instr.InstrNo = instrCounter;
                var csInstr = instr.GetInstrumentScript(instrCounter, $"INSTRUMENT_{instrCounter}");
                instrBuilder.AppendLine(csInstr);
                var evtSec = instr.GetEventsScript(instrCounter, $"INSTRUMENT_{instrCounter}");
                foreach(var evt in evtSec)
                {
                    eventsBuilder.AppendLine(evt);
                }
                instrCounter += 100;
            }

            var all = GetAll(instrBuilder.ToString(), eventsBuilder.ToString());
            return all;
        }



        public static string GetCsOptions()
        {
            var part = @"
<CsOptions>
		; Select audio/midi flags here according to platform
		-odac  ;;;realtime audio out
		; -iadc    ;;;uncomment -iadc if real audio input is needed too
		; For Non-realtime ouput leave only the line below:
		; -o sr.wav -W ;;; for file output any platform
</CsOptions>";
            return part;
        }

        public static string GetKSMPS()
        {
            var part = $@"
		        sr = 44100
		        ksmps = 10
		        nchnls = 2
		        0dbfs  = 1";
            return part;
        }

        public static string GetNoiseInstrument(int instrNo)
        {
            var part = $@"
        ;#########################################################################    
        ;### NOISE NOISE NOISE NOISE NOISE NOISE NOISE NOISE NOISE NOISE NOISE  ##    
        ;#########################################################################    
        instr {instrNo}
            kEnv linen p4, 0.1, p3, 0.1  ; Create an envelope for amplitude control
            aNoise rand -1, 1            ; Generate white noise
            aOut = aNoise * kEnv          ; Apply envelope to noise
            outs aOut, aOut               ; Output to stereo channels
        endin";
            return part;
        }
        public static string GetSampleInstrument(int instrNo, string instrName, string path)
        {
            var part = 
                @$"
                ;##########################################################
                ;         {instrNo} {instrName}                          
                ;##########################################################
                instr {instrNo}
                    aLeft, aRight diskin2 ""{path}"", 1, p4
                    aOutL atone aLeft,  230
                    aOutR atone aRight, 230
                    outs 0.2*aOutL, 0.2*aOutR  ; Output to both stereo channels
                endin";
            return part;
        }

        public static string GetSimpleScript()
        {
            var path = @"C:/Users/slonj/Desktop/Samples/Bowed_MP-Viole.wav";
            var program = @$"
<CsoundSynthesizer>
{GetCsOptions()}
<CsInstruments>
		{GetKSMPS()}
        {GetNoiseInstrument(99)}
        {GetSampleInstrument(3, "abc", path)}
</CsInstruments>
<CsScore>
	f1 0 4096 10 1	;sine wave
    ;i 3 0 1000 6  ; Play myFile.wav starting at 0 seconds for 10 seconds
    ;i 4 0 10000  ;
    i99 0 36000 0.0003 ; i99 start at 0, 10 hours long, amp .0001
	e
</CsScore>
</CsoundSynthesizer>
";
			return program;
        }

        public static string GetAll(string allInstruments, string allEvents)
        {
            var program = @$"
<CsoundSynthesizer>
{GetCsOptions()}
<CsInstruments>
{GetKSMPS()}
{allInstruments}
</CsInstruments>
<CsScore>
	f1 0 4096 10 1	;sine wave
    ;i 3 0 1000 6  ; Play myFile.wav starting at 0 seconds for 10 seconds
    ;i 4 0 10000  ;
    ;i99 0 36000 0.0003 ; i99 start at 0, 10 hours long, amp .0001
{allEvents}
	e
</CsScore>
</CsoundSynthesizer>
            ";
            return program;
        }
    }
}

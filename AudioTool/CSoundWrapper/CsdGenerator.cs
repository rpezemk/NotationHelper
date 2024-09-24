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
		public static string GetSampleInstrument(string path, int instrNo)
		{
            string instrument = $@"        
            instr {instrNo}
                aLeft, aRight diskin2 ""{path}"", 1, p4  ; Play the WAV file
                aOutL atone aLeft,  230
                aOutR atone aRight, 230
                outs 0.2*aOutL, 0.2*aOutR  ; Output to both stereo channels
            endin";
			return instrument;
        }


        public static string GetSimpleProgram()
        {
            var path = @"C:/Users/slonj/Desktop/Samples/Bowed_MP-Viole.wav";
            var program = @$"
<CsoundSynthesizer>
	<CsOptions>
		; Select audio/midi flags here according to platform
		-odac  ;;;realtime audio out
		; -iadc    ;;;uncomment -iadc if real audio input is needed too
		; For Non-realtime ouput leave only the line below:
		; -o sr.wav -W ;;; for file output any platform
	</CsOptions>
	<CsInstruments>
		sr = 44100
		ksmps = 10
		nchnls = 2
		0dbfs  = 1

		instr 1
            kAmp = 0.0002  ; Fixed amplitude value
            kenv linen kAmp, 0.1, p3 - 0.1, 0.5  ; Attack: 0.1s, Sustain: p3-0.1s, Release: 0.2s
            aSig oscil kenv, p4, 1  ; p4 is the frequency of the sine wave, 1 is the table number
            outs aSig, aSig
        endin


        ; Instrument 3 - Play WAV File
        instr 3
            aLeft, aRight diskin2 ""{path}"", 1, p4  ; Play the WAV file
            aOutL atone aLeft,  230
            aOutR atone aRight, 230
            outs 0.2*aOutL, 0.2*aOutR  ; Output to both stereo channels
        endin

        

		instr 10	
		    kAmp chnget ""amplitude""         ; Get current value from the control channel
		    kNewAmp = abs(sin(oscili(0.1, 0.1))) + 0.1 ; Example: sine modulation of amplitude
		    chnset kNewAmp, ""amplitude""

		    prints ""amplitude = %f \n"", kNewAmp   ; Print the current amplitude value
		endin

        instr 99
            kEnv linen p4, 0.1, p3, 0.1  ; Create an envelope for amplitude control
            aNoise rand -1, 1            ; Generate white noise
            aOut = aNoise * kEnv          ; Apply envelope to noise
            outs aOut, aOut               ; Output to stereo channels
        endin
	

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
    }
}

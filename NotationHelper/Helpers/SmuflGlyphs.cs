using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace NotationHelper.Helpers
{
    public static class SmuflGlyphs
    {
        public static string GetPackUri()
        {
            string folderPath = "FontResourcses";
            string fontFamilyName = "Bravura.otf";

            //string packUri = "pack://application:,,,/" + assemblyName + ";component/" + folderPath + "/#" + fontFamilyName;
            string packUri = "pack://application:,,,./" + folderPath + "/" + fontFamilyName;
            return packUri;
        }
    }

    public static class ConstGlyphs
    {
        // Clefs
        public const string G_Clef = "\uE050";
        public const string C_Clef = "\uE05C";
        public const string F_Clef = "\uE062";
        public const string Percussion_Clef_1 = "\uE069";
        public const string Percussion_Clef_2 = "\uE06A";

        // Noteheads
        public const string Notehead_Black = "\uE0A4";
        public const string Notehead_Half = "\uE0A3";
        public const string Notehead_Whole = "\uE0A2";
        public const string Notehead_DoubleWhole = "\uE0A0";
        public const string Notehead_X = "\uE0A7";

        // Rests
        public const string Rest_Half = "\uE4E5";
        public const string Rest_Quarter = "\uE4E6";
        public const string Rest_Eighth = "\uE4E7";
        public const string Rest_Sixteenth = "\uE4E8";
        public const string Rest_Whole = "\uE4E4";
        public const string Rest_DoubleWhole = "\uE4E3";

        // Accidentals
        public const string Accidental_Natural = "\uE260";
        public const string Accidental_Flat = "\uE261";
        public const string Accidental_Sharp = "\uE262";
        public const string Accidental_DoubleFlat = "\uE264";
        public const string Accidental_DoubleSharp = "\uE263";

        // Time Signatures
        public const string TimeSig_4_4 = "\uE08A";
        public const string TimeSig_3_4 = "\uE086";
        public const string TimeSig_2_4 = "\uE082";
        public const string TimeSig_6_8 = "\uE09C";
        public const string TimeSig_C = "\uE080";
        public const string TimeSig_CutC = "\uE081";

        // Articulations
        public const string Staccato = "\uE4A0";
        public const string Tenuto = "\uE4A3";
        public const string Accent = "\uE4AC";
        public const string Marcato = "\uE4A5";
        public const string Fermata = "\uE4C0";

        // Dynamics
        public const string Dynamic_Piano = "\uE520";
        public const string Dynamic_MezzoPiano = "\uE521";
        public const string Dynamic_MezzoForte = "\uE522";
        public const string Dynamic_Forte = "\uE523";
        public const string Dynamic_Fortissimo = "\uE524";
        public const string Dynamic_Pianissimo = "\uE525";

        // Bar Lines
        public const string Barline_Single = "\uE030";
        public const string Barline_Double = "\uE031";
        public const string Barline_Final = "\uE032";
        public const string Barline_Repeat_Left = "\uE040";
        public const string Barline_Repeat_Right = "\uE041";

        // Tuplets
        public const string Tuplet_3 = "\uE883";
        public const string Tuplet_6 = "\uE886";

        // Ornaments
        public const string Trill = "\uE566";
        public const string Mordent = "\uE56A";
        public const string Turn = "\uE56E";
        public const string InvertedTurn = "\uE570";

        // Arpeggios
        public const string Arpeggio_Up = "\uE635";
        public const string Arpeggio_Down = "\uE636";

        // Octaves
        public const string Octave_8va = "\uE510";
        public const string Octave_15ma = "\uE512";
        public const string Octave_22ma = "\uE514";

        // Breath Marks
        public const string BreathMark_Comma = "\uE4CE";
        public const string BreathMark_Tick = "\uE4CF";

        // Flags (Stems)
        public const string Flag_EighthUp = "\uE240";
        public const string Flag_EighthDown = "\uE242";
        public const string Flag_SixteenthUp = "\uE244";
        public const string Flag_SixteenthDown = "\uE246";

        // Repeat Endings
        public const string RepeatEnding1 = "\uE00D";
        public const string RepeatEnding2 = "\uE00E";

        // Slurs and Ties
        public const string Slur = "\uE010";
        public const string Tie = "\uE015";

        // Grace Notes
        public const string GraceNoteSlash = "\uE2D4";
        public const string GraceNoteAcciaccatura = "\uE563";

        // Glissando/Slide
        public const string Glissando = "\uE580";
        public const string SlideIn = "\uE5B0";
        public const string SlideOut = "\uE5B1";

        // Tremolos
        public const string Tremolo_OneStroke = "\uE220";
        public const string Tremolo_TwoStrokes = "\uE221";
        public const string Tremolo_ThreeStrokes = "\uE222";

        // Repeats and Codas
        public const string Segno = "\uE047";
        public const string Coda = "\uE048";
        public const string DalSegno = "\uE045";
        public const string DaCapo = "\uE046";
        public const string Fine = "\uE044";

        // Pedals
        public const string PedalMark = "\uE650";
        public const string PedalUpMark = "\uE653";

        // Brackets
        public const string Bracket = "\uE002";
        public const string Brace = "\uE003";

        // Accents
        public const string MarcatoStaccato = "\uE4B4";
        public const string Staccatissimo = "\uE4A7";
        public const string TenutoStaccato = "\uE4A6";

        // Ottavas
        public const string OttavaAlta = "\uE510";
        public const string OttavaBassa = "\uE511";

        // Fingering
        public const string Fingering_1 = "\uE550";
        public const string Fingering_2 = "\uE551";
        public const string Fingering_3 = "\uE552";
        public const string Fingering_4 = "\uE553";
        public const string Fingering_5 = "\uE554";

        // Brackets
        public const string StartSquareBracket = "\uE004";
        public const string EndSquareBracket = "\uE005";

        // Measure Rests
        public const string MeasureRest = "\uE4E3";

        // Repeat Signs
        public const string RepeatDot = "\uE043";

        // Stem Up/Down
        public const string StemUp = "\uE210";
        public const string StemDown = "\uE211";

        // Instrument Techniques
        public const string BowingUp = "\uE610";
        public const string BowingDown = "\uE611";

        // Multiple Staff Brackets
        public const string BraceStaff = "\uE00F";

        // Fermata
        public const string Fermata_Upright = "\uE4C0";
        public const string Fermata_Inverted = "\uE4C1";

        // Dynamics (more detailed)
        public const string Forte = "\uE52F";
        public const string Piano = "\uE530";
        public const string Fortissimo = "\uE52A";
        public const string Pianissimo = "\uE52B";

        // Various Notes
        public const string BreveNote = "\uE1D1";
        public const string WholeNote = "\uE1D2";
        public const string HalfNote = "\uE1D3";
        public const string QuarterNote = "\uE1D5";
        public const string EighthNote = "\uE1D7";
        public const string SixteenthNote = "\uE1D9";

        // Additional symbols can be added as required...
    }

}

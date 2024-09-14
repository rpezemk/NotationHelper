﻿using NotationHelper.DataModel.Elementary;
using NotationHelper.DataModel.Piece;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotationHelper.Helpers
{
    public static class NoteHelper
    {
        public static Note C(this Note note) => note.SetName(DataModel.Elementary.NoteName.C);
        public static Note D(this Note note) => note.SetName(DataModel.Elementary.NoteName.D);
        public static Note E(this Note note) => note.SetName(DataModel.Elementary.NoteName.E);
        public static Note F(this Note note) => note.SetName(DataModel.Elementary.NoteName.F);
        public static Note G(this Note note) => note.SetName(DataModel.Elementary.NoteName.G);
        public static Note A(this Note note) => note.SetName(DataModel.Elementary.NoteName.A);
        public static Note B(this Note note) => note.SetName(DataModel.Elementary.NoteName.B);

        
        
        public static TimeGroup Whole(this TimeGroup timeGroup) => timeGroup.SetBaseDuration(DataModel.Elementary.DurationEnum.Whole);
        public static TimeGroup Half(this TimeGroup timeGroup) => timeGroup.SetBaseDuration(DataModel.Elementary.DurationEnum.Half);
        public static TimeGroup Quarter(this TimeGroup timeGroup) => timeGroup.SetBaseDuration(DataModel.Elementary.DurationEnum.Querter);
        public static TimeGroup Eight(this TimeGroup timeGroup) => timeGroup.SetBaseDuration(DataModel.Elementary.DurationEnum.Eight);
        public static TimeGroup Sixteen(this TimeGroup timeGroup) => timeGroup.SetBaseDuration(DataModel.Elementary.DurationEnum.Sixteen);
        public static TimeGroup ThirtyTwo(this TimeGroup timeGroup) => timeGroup.SetBaseDuration(DataModel.Elementary.DurationEnum.ThirtyTwo);

        public static Note Whole(this Note timeGroup) => timeGroup.SetBaseDuration(DataModel.Elementary.DurationEnum.Whole);
        public static Note Half(this Note timeGroup) => timeGroup.SetBaseDuration(DataModel.Elementary.DurationEnum.Half);
        public static Note Quarter(this Note timeGroup) => timeGroup.SetBaseDuration(DataModel.Elementary.DurationEnum.Querter);
        public static Note Eight(this Note timeGroup) => timeGroup.SetBaseDuration(DataModel.Elementary.DurationEnum.Eight);
        public static Note Sixteen(this Note timeGroup) => timeGroup.SetBaseDuration(DataModel.Elementary.DurationEnum.Sixteen);
        public static Note ThirtyTwo(this Note timeGroup) => timeGroup.SetBaseDuration(DataModel.Elementary.DurationEnum.ThirtyTwo);

        public static Note UpOct(this Note note) => note.AlterOctave(1);
        public static Note DownOct(this Note note) => note.AlterOctave(-1);


        public static TimeGroup Dot(this TimeGroup timeGroup) => timeGroup.SetDotting(DataModel.Elementary.DottingEnum.SingleDot);
        public static TimeGroup DoubleDot(this TimeGroup timeGroup) => timeGroup.SetDotting(DataModel.Elementary.DottingEnum.DoubleDot);



        public static Note Sharp(this Note note) => note.SetAlter(1);
        public static Note Flat(this Note note) => note.SetAlter(-1);

    }
}

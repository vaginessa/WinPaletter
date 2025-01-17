﻿using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WinPaletter.Theme;
using WinPaletter.UI.Controllers;

namespace WinPaletter
{
    public partial class CursorsStudio
    {
        private bool _Shown = false;
        private CursorControl _SelectedControl;
        private CursorControl _CopiedControl;
        private readonly List<CursorControl> AnimateList = new();

        public CursorsStudio()
        {
            InitializeComponent();
        }

        public void CursorTM_to_Cursor(CursorControl CursorControl, Theme.Structures.Cursor Cursor)
        {
            CursorControl.Prop_UseFromFile = Cursor.UseFromFile;
            CursorControl.Prop_File = Cursor.File;
            CursorControl.Prop_ArrowStyle = Cursor.ArrowStyle;
            CursorControl.Prop_CircleStyle = Cursor.CircleStyle;
            CursorControl.Prop_PrimaryColor1 = Cursor.PrimaryColor1;
            CursorControl.Prop_PrimaryColor2 = Cursor.PrimaryColor2;
            CursorControl.Prop_PrimaryColorGradient = Cursor.PrimaryColorGradient;
            CursorControl.Prop_PrimaryColorGradientMode = Cursor.PrimaryColorGradientMode;
            CursorControl.Prop_PrimaryNoise = Cursor.PrimaryColorNoise;
            CursorControl.Prop_PrimaryNoiseOpacity = Cursor.PrimaryColorNoiseOpacity;
            CursorControl.Prop_SecondaryColor1 = Cursor.SecondaryColor1;
            CursorControl.Prop_SecondaryColor2 = Cursor.SecondaryColor2;
            CursorControl.Prop_SecondaryColorGradient = Cursor.SecondaryColorGradient;
            CursorControl.Prop_SecondaryColorGradientMode = Cursor.SecondaryColorGradientMode;
            CursorControl.Prop_SecondaryNoise = Cursor.SecondaryColorNoise;
            CursorControl.Prop_SecondaryNoiseOpacity = Cursor.SecondaryColorNoiseOpacity;
            CursorControl.Prop_LoadingCircleBack1 = Cursor.LoadingCircleBack1;
            CursorControl.Prop_LoadingCircleBack2 = Cursor.LoadingCircleBack2;
            CursorControl.Prop_LoadingCircleBackGradient = Cursor.LoadingCircleBackGradient;
            CursorControl.Prop_LoadingCircleBackGradientMode = Cursor.LoadingCircleBackGradientMode;
            CursorControl.Prop_LoadingCircleBackNoise = Cursor.LoadingCircleBackNoise;
            CursorControl.Prop_LoadingCircleBackNoiseOpacity = Cursor.LoadingCircleBackNoiseOpacity;
            CursorControl.Prop_LoadingCircleHot1 = Cursor.LoadingCircleHot1;
            CursorControl.Prop_LoadingCircleHot2 = Cursor.LoadingCircleHot2;
            CursorControl.Prop_LoadingCircleHotGradient = Cursor.LoadingCircleHotGradient;
            CursorControl.Prop_LoadingCircleHotGradientMode = Cursor.LoadingCircleHotGradientMode;
            CursorControl.Prop_LoadingCircleHotNoise = Cursor.LoadingCircleHotNoise;
            CursorControl.Prop_LoadingCircleHotNoiseOpacity = Cursor.LoadingCircleHotNoiseOpacity;
            CursorControl.Prop_Shadow_Enabled = Cursor.Shadow_Enabled;
            CursorControl.Prop_Shadow_Color = Cursor.Shadow_Color;
            CursorControl.Prop_Shadow_Blur = Cursor.Shadow_Blur;
            CursorControl.Prop_Shadow_Opacity = Cursor.Shadow_Opacity;
            CursorControl.Prop_Shadow_OffsetX = Cursor.Shadow_OffsetX;
            CursorControl.Prop_Shadow_OffsetY = Cursor.Shadow_OffsetY;
        }

        public Theme.Structures.Cursor Cursor_to_CursorTM(CursorControl CursorControl)
        {
            Theme.Structures.Cursor Cursor;
            Cursor.UseFromFile = CursorControl.Prop_UseFromFile;
            Cursor.File = CursorControl.Prop_File;
            Cursor.ArrowStyle = CursorControl.Prop_ArrowStyle;
            Cursor.CircleStyle = CursorControl.Prop_CircleStyle;
            Cursor.PrimaryColor1 = CursorControl.Prop_PrimaryColor1;
            Cursor.PrimaryColor2 = CursorControl.Prop_PrimaryColor2;
            Cursor.PrimaryColorGradient = CursorControl.Prop_PrimaryColorGradient;
            Cursor.PrimaryColorGradientMode = CursorControl.Prop_PrimaryColorGradientMode;
            Cursor.PrimaryColorNoise = CursorControl.Prop_PrimaryNoise;
            Cursor.PrimaryColorNoiseOpacity = CursorControl.Prop_PrimaryNoiseOpacity;
            Cursor.SecondaryColor1 = CursorControl.Prop_SecondaryColor1;
            Cursor.SecondaryColor2 = CursorControl.Prop_SecondaryColor2;
            Cursor.SecondaryColorGradient = CursorControl.Prop_SecondaryColorGradient;
            Cursor.SecondaryColorGradientMode = CursorControl.Prop_SecondaryColorGradientMode;
            Cursor.SecondaryColorNoise = CursorControl.Prop_SecondaryNoise;
            Cursor.SecondaryColorNoiseOpacity = CursorControl.Prop_SecondaryNoiseOpacity;
            Cursor.LoadingCircleBack1 = CursorControl.Prop_LoadingCircleBack1;
            Cursor.LoadingCircleBack2 = CursorControl.Prop_LoadingCircleBack2;
            Cursor.LoadingCircleBackGradient = CursorControl.Prop_LoadingCircleBackGradient;
            Cursor.LoadingCircleBackGradientMode = CursorControl.Prop_LoadingCircleBackGradientMode;
            Cursor.LoadingCircleBackNoise = CursorControl.Prop_LoadingCircleBackNoise;
            Cursor.LoadingCircleBackNoiseOpacity = CursorControl.Prop_LoadingCircleBackNoiseOpacity;
            Cursor.LoadingCircleHot1 = CursorControl.Prop_LoadingCircleHot1;
            Cursor.LoadingCircleHot2 = CursorControl.Prop_LoadingCircleHot2;
            Cursor.LoadingCircleHotGradient = CursorControl.Prop_LoadingCircleHotGradient;
            Cursor.LoadingCircleHotGradientMode = CursorControl.Prop_LoadingCircleHotGradientMode;
            Cursor.LoadingCircleHotNoise = CursorControl.Prop_LoadingCircleHotNoise;
            Cursor.LoadingCircleHotNoiseOpacity = CursorControl.Prop_LoadingCircleHotNoiseOpacity;
            Cursor.Shadow_Enabled = CursorControl.Prop_Shadow_Enabled;
            Cursor.Shadow_Color = CursorControl.Prop_Shadow_Color;
            Cursor.Shadow_Blur = CursorControl.Prop_Shadow_Blur;
            Cursor.Shadow_Opacity = CursorControl.Prop_Shadow_Opacity;
            Cursor.Shadow_OffsetX = CursorControl.Prop_Shadow_OffsetX;
            Cursor.Shadow_OffsetY = CursorControl.Prop_Shadow_OffsetY;

            return Cursor;
        }

        public void LoadFromTM(Theme.Manager TM)
        {
            Toggle1.Checked = TM.Cursor_Enabled;
            CheckBox9.Checked = TM.Cursor_Shadow;
            Trackbar2.Value = TM.Cursor_Trails;
            trails_btn.Text = TM.Cursor_Trails.ToString();
            CheckBox10.Checked = TM.Cursor_Sonar;
            CursorTM_to_Cursor(Arrow, TM.Cursor_Arrow);
            CursorTM_to_Cursor(Help, TM.Cursor_Help);
            CursorTM_to_Cursor(AppLoading, TM.Cursor_AppLoading);
            CursorTM_to_Cursor(Busy, TM.Cursor_Busy);
            CursorTM_to_Cursor(Move_Cur, TM.Cursor_Move);
            CursorTM_to_Cursor(NS, TM.Cursor_NS);
            CursorTM_to_Cursor(EW, TM.Cursor_EW);
            CursorTM_to_Cursor(NESW, TM.Cursor_NESW);
            CursorTM_to_Cursor(NWSE, TM.Cursor_NWSE);
            CursorTM_to_Cursor(Up, TM.Cursor_Up);
            CursorTM_to_Cursor(Pen, TM.Cursor_Pen);
            CursorTM_to_Cursor(None, TM.Cursor_None);
            CursorTM_to_Cursor(Link, TM.Cursor_Link);
            CursorTM_to_Cursor(Pin, TM.Cursor_Pin);
            CursorTM_to_Cursor(Person, TM.Cursor_Person);
            CursorTM_to_Cursor(IBeam, TM.Cursor_IBeam);
            CursorTM_to_Cursor(Cross, TM.Cursor_Cross);

            foreach (CursorControl i in FlowLayoutPanel1.Controls.OfType<CursorControl>())
            {
                i.Invalidate();
            }
        }

        public void SaveToTM(Theme.Manager TM)
        {
            TM.Cursor_Enabled = Toggle1.Checked;
            TM.Cursor_Shadow = CheckBox9.Checked;
            TM.Cursor_Trails = Trackbar2.Value;
            TM.Cursor_Sonar = CheckBox10.Checked;
            TM.Cursor_Arrow = Cursor_to_CursorTM(Arrow);
            TM.Cursor_Help = Cursor_to_CursorTM(Help);
            TM.Cursor_AppLoading = Cursor_to_CursorTM(AppLoading);
            TM.Cursor_Busy = Cursor_to_CursorTM(Busy);
            TM.Cursor_Move = Cursor_to_CursorTM(Move_Cur);
            TM.Cursor_NS = Cursor_to_CursorTM(NS);
            TM.Cursor_EW = Cursor_to_CursorTM(EW);
            TM.Cursor_NESW = Cursor_to_CursorTM(NESW);
            TM.Cursor_NWSE = Cursor_to_CursorTM(NWSE);
            TM.Cursor_Up = Cursor_to_CursorTM(Up);
            TM.Cursor_Pen = Cursor_to_CursorTM(Pen);
            TM.Cursor_None = Cursor_to_CursorTM(None);
            TM.Cursor_Link = Cursor_to_CursorTM(Link);
            TM.Cursor_Pin = Cursor_to_CursorTM(Pin);
            TM.Cursor_Person = Cursor_to_CursorTM(Person);
            TM.Cursor_IBeam = Cursor_to_CursorTM(IBeam);
            TM.Cursor_Cross = Cursor_to_CursorTM(Cross);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.LoadLanguage();
            ApplyStyle(this);
            FlowLayoutPanel1.DoubleBuffer();

            _CopiedControl = null;
            _Shown = false;

            Angle = 180f;
            Cycles = 0;
            Timer1.Enabled = false;
            Timer1.Stop();

            Button8.Image = Forms.MainFrm.Button20.Image.Resize(16, 16);

            LoadFromTM(Program.TM);

            // Remove handler to avoid doubling/tripling events
            foreach (CursorControl i in FlowLayoutPanel1.Controls.OfType<CursorControl>())
            {
                try { i.Click -= Clicked; } catch { }
            }

            foreach (CursorControl i in FlowLayoutPanel1.Controls.OfType<CursorControl>())
            {
                i.Click += Clicked;
            }
        }

        protected override void OnDragOver(DragEventArgs e)
        {
            if (e.Data.GetData(typeof(UI.Controllers.ColorItem).FullName) is UI.Controllers.ColorItem)
            {
                Focus();
                BringToFront();
            }
            else
            {
                return;
            }

            base.OnDragOver(e);
        }

        private void CursorsStudio_Shown(object sender, EventArgs e)
        {
            _Shown = true;
        }

        public void Clicked(object sender, EventArgs e)
        {
            _SelectedControl = (CursorControl)sender;
            ApplyColorsFromCursor(_SelectedControl);
            Button1.Enabled = true;
            if (!tabControl1.Visible) Program.Animator.ShowSync(tabControl1);
        }

        public void ApplyColorsFromCursor(CursorControl CursorControl)
        {
            source1.Checked = CursorControl.Prop_UseFromFile;
            source0.Checked = !CursorControl.Prop_UseFromFile;
            textBox1.Text = CursorControl.Prop_File;

            ComboBox5.SelectedIndex = (int)CursorControl.Prop_ArrowStyle;
            ComboBox6.SelectedIndex = (int)CursorControl.Prop_CircleStyle;

            PrimaryColor1.BackColor = CursorControl.Prop_PrimaryColor1;
            PrimaryColor2.BackColor = CursorControl.Prop_PrimaryColor2;
            CheckBox1.Checked = CursorControl.Prop_PrimaryColorGradient;
            ComboBox1.SelectedItem = Paths.ReturnStringFromGradientMode(CursorControl.Prop_PrimaryColorGradientMode);
            CheckBox5.Checked = CursorControl.Prop_PrimaryNoise;
            Trackbar3.Value = (int)Math.Round(CursorControl.Prop_PrimaryNoiseOpacity * 100f);

            SecondaryColor1.BackColor = CursorControl.Prop_SecondaryColor1;
            SecondaryColor2.BackColor = CursorControl.Prop_SecondaryColor2;
            CheckBox4.Checked = CursorControl.Prop_SecondaryColorGradient;
            ComboBox2.SelectedItem = Paths.ReturnStringFromGradientMode(CursorControl.Prop_SecondaryColorGradientMode);
            CheckBox3.Checked = CursorControl.Prop_SecondaryNoise;
            Trackbar4.Value = (int)Math.Round(CursorControl.Prop_SecondaryNoiseOpacity * 100f);

            CircleColor1.BackColor = CursorControl.Prop_LoadingCircleBack1;
            CircleColor2.BackColor = CursorControl.Prop_LoadingCircleBack2;
            CheckBox8.Checked = CursorControl.Prop_LoadingCircleBackGradient;
            ComboBox4.SelectedItem = Paths.ReturnStringFromGradientMode(CursorControl.Prop_LoadingCircleBackGradientMode);
            CheckBox7.Checked = CursorControl.Prop_LoadingCircleBackNoise;
            Trackbar5.Value = (int)Math.Round(CursorControl.Prop_LoadingCircleBackNoiseOpacity * 100f);

            LoadingColor1.BackColor = CursorControl.Prop_LoadingCircleHot1;
            LoadingColor2.BackColor = CursorControl.Prop_LoadingCircleHot2;
            CheckBox2.Checked = CursorControl.Prop_LoadingCircleHotGradient;
            ComboBox3.SelectedItem = Paths.ReturnStringFromGradientMode(CursorControl.Prop_LoadingCircleHotGradientMode);
            CheckBox6.Checked = CursorControl.Prop_LoadingCircleHotNoise;
            Trackbar6.Value = (int)Math.Round(CursorControl.Prop_LoadingCircleHotNoiseOpacity * 100f);

            CheckBox11.Checked = CursorControl.Prop_Shadow_Enabled;
            ColorItem1.BackColor = CursorControl.Prop_Shadow_Color;
            Trackbar7.Value = CursorControl.Prop_Shadow_Blur;
            Trackbar8.Value = (int)Math.Round(CursorControl.Prop_Shadow_Opacity * 100f);
            Trackbar9.Value = CursorControl.Prop_Shadow_OffsetX;
            Trackbar10.Value = CursorControl.Prop_Shadow_OffsetY;
        }

        public void ApplyColorsToPreview(CursorControl CursorControl)
        {
            CursorControl.Prop_UseFromFile = source1.Checked;
            CursorControl.Prop_File = textBox1.Text;

            CursorControl.Prop_ArrowStyle = (Paths.ArrowStyle)ComboBox5.SelectedIndex;
            CursorControl.Prop_CircleStyle = (Paths.CircleStyle)ComboBox6.SelectedIndex;

            CursorControl.Prop_PrimaryColor1 = PrimaryColor1.BackColor;
            CursorControl.Prop_PrimaryColor2 = PrimaryColor2.BackColor;
            CursorControl.Prop_PrimaryColorGradient = CheckBox1.Checked;
            CursorControl.Prop_PrimaryColorGradientMode = Paths.ReturnGradientModeFromString(ComboBox1.SelectedItem.ToString());
            CursorControl.Prop_PrimaryNoise = CheckBox5.Checked;
            CursorControl.Prop_PrimaryNoiseOpacity = (float)(Trackbar3.Value / 100d);

            CursorControl.Prop_SecondaryColor1 = SecondaryColor1.BackColor;
            CursorControl.Prop_SecondaryColor2 = SecondaryColor2.BackColor;
            CursorControl.Prop_SecondaryColorGradient = CheckBox4.Checked;
            CursorControl.Prop_SecondaryColorGradientMode = Paths.ReturnGradientModeFromString(ComboBox2.SelectedItem.ToString());
            CursorControl.Prop_SecondaryNoise = CheckBox3.Checked;
            CursorControl.Prop_SecondaryNoiseOpacity = (float)(Trackbar4.Value / 100d);

            CursorControl.Prop_LoadingCircleBack1 = CircleColor1.BackColor;
            CursorControl.Prop_LoadingCircleBack2 = CircleColor2.BackColor;
            CursorControl.Prop_LoadingCircleBackGradient = CheckBox8.Checked;
            CursorControl.Prop_LoadingCircleBackGradientMode = Paths.ReturnGradientModeFromString(ComboBox4.SelectedItem.ToString());
            CursorControl.Prop_LoadingCircleBackNoise = CheckBox7.Checked;
            CursorControl.Prop_LoadingCircleBackNoiseOpacity = (float)(Trackbar5.Value / 100d);

            CursorControl.Prop_LoadingCircleHot1 = LoadingColor1.BackColor;
            CursorControl.Prop_LoadingCircleHot2 = LoadingColor2.BackColor;
            CursorControl.Prop_LoadingCircleHotGradient = CheckBox2.Checked;
            CursorControl.Prop_LoadingCircleHotGradientMode = Paths.ReturnGradientModeFromString(ComboBox3.SelectedItem.ToString());
            CursorControl.Prop_LoadingCircleHotNoise = CheckBox6.Checked;
            CursorControl.Prop_LoadingCircleHotNoiseOpacity = (float)(Trackbar6.Value / 100d);

            CursorControl.Prop_Shadow_Enabled = CheckBox11.Checked;
            CursorControl.Prop_Shadow_Color = ColorItem1.BackColor;
            CursorControl.Prop_Shadow_Blur = Trackbar7.Value;
            CursorControl.Prop_Shadow_Opacity = (float)(Trackbar8.Value / 100d);
            CursorControl.Prop_Shadow_OffsetX = Trackbar9.Value;
            CursorControl.Prop_Shadow_OffsetY = Trackbar10.Value;

        }

        private void TaskbarFrontAndFoldersOnStart_picker_Click(object sender, EventArgs e)
        {
            if (e is DragEventArgs)
            {
                _SelectedControl.Prop_PrimaryColor1 = ((ColorItem)sender).BackColor;
                _SelectedControl.Invalidate();
                return;
            }

            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                Forms.SubMenu.ShowMenu((UI.Controllers.ColorItem)sender);
                if (ColorClipboard.Event == ColorClipboard.MenuEvent.Cut | ColorClipboard.Event == ColorClipboard.MenuEvent.Paste | ColorClipboard.Event == ColorClipboard.MenuEvent.Override)
                {
                    _SelectedControl.Prop_PrimaryColor1 = ((ColorItem)sender).BackColor;
                    _SelectedControl.Invalidate();
                }
                return;
            }

            List<Control> CList = new() { (UI.Controllers.ColorItem)sender, _SelectedControl };

            Conditions _conditions = new() { CursorBack1 = true, Win7 = false, LivePreview_AfterGlow = false, LivePreview_Colorization = false };
            Color C = Forms.ColorPickerDlg.Pick(CList, _conditions, true);

            _SelectedControl.Prop_PrimaryColor1 = C;
            _SelectedControl.Invalidate();

            ((UI.Controllers.ColorItem)sender).BackColor = C;
            ((UI.Controllers.ColorItem)sender).Invalidate();

            CList.Clear();
        }

        private void GroupBox3_Click(object sender, EventArgs e)
        {

            if (e is DragEventArgs)
            {
                _SelectedControl.Prop_PrimaryColor2 = ((ColorItem)sender).BackColor;
                _SelectedControl.Invalidate();
                return;
            }

            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                Forms.SubMenu.ShowMenu((UI.Controllers.ColorItem)sender);
                if (ColorClipboard.Event == ColorClipboard.MenuEvent.Cut | ColorClipboard.Event == ColorClipboard.MenuEvent.Paste | ColorClipboard.Event == ColorClipboard.MenuEvent.Override)
                {
                    _SelectedControl.Prop_PrimaryColor2 = ((ColorItem)sender).BackColor;
                    _SelectedControl.Invalidate();
                }
                return;
            }

            List<Control> CList = new() { (UI.Controllers.ColorItem)sender, _SelectedControl };

            Conditions _conditions = new() { CursorBack2 = true, Win7 = false, LivePreview_AfterGlow = false, LivePreview_Colorization = false };
            Color C = Forms.ColorPickerDlg.Pick(CList, _conditions, true);

            _SelectedControl.Prop_PrimaryColor2 = C;
            _SelectedControl.Invalidate();
            ((UI.Controllers.ColorItem)sender).BackColor = C;
            ((UI.Controllers.ColorItem)sender).Invalidate();

            CList.Clear();

        }

        private void GroupBox5_Click(object sender, EventArgs e)
        {

            if (e is DragEventArgs)
            {
                _SelectedControl.Prop_SecondaryColor1 = ((ColorItem)sender).BackColor;
                _SelectedControl.Invalidate();
                return;
            }

            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                Forms.SubMenu.ShowMenu((UI.Controllers.ColorItem)sender);
                if (ColorClipboard.Event == ColorClipboard.MenuEvent.Cut | ColorClipboard.Event == ColorClipboard.MenuEvent.Paste | ColorClipboard.Event == ColorClipboard.MenuEvent.Override)
                {
                    _SelectedControl.Prop_SecondaryColor1 = ((ColorItem)sender).BackColor;
                    _SelectedControl.Invalidate();
                }
                return;
            }

            List<Control> CList = new() { (UI.Controllers.ColorItem)sender, _SelectedControl };

            Conditions _conditions = new() { CursorLine1 = true, Win7 = false, LivePreview_AfterGlow = false, LivePreview_Colorization = false };
            Color C = Forms.ColorPickerDlg.Pick(CList, _conditions, true);

            _SelectedControl.Prop_SecondaryColor1 = C;
            _SelectedControl.Invalidate();
            ((UI.Controllers.ColorItem)sender).BackColor = C;
            ((UI.Controllers.ColorItem)sender).Invalidate();

            CList.Clear();

        }

        private void GroupBox4_Click(object sender, EventArgs e)
        {

            if (e is DragEventArgs)
            {
                _SelectedControl.Prop_SecondaryColor2 = ((ColorItem)sender).BackColor;
                _SelectedControl.Invalidate();
                return;
            }

            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                Forms.SubMenu.ShowMenu((UI.Controllers.ColorItem)sender);
                if (ColorClipboard.Event == ColorClipboard.MenuEvent.Cut | ColorClipboard.Event == ColorClipboard.MenuEvent.Paste | ColorClipboard.Event == ColorClipboard.MenuEvent.Override)
                {
                    _SelectedControl.Prop_SecondaryColor2 = ((ColorItem)sender).BackColor;
                    _SelectedControl.Invalidate();
                }
                return;
            }

            List<Control> CList = new() { (UI.Controllers.ColorItem)sender, _SelectedControl };

            Conditions _conditions = new() { CursorLine2 = true, Win7 = false, LivePreview_AfterGlow = false, LivePreview_Colorization = false };
            Color C = Forms.ColorPickerDlg.Pick(CList, _conditions, true);

            _SelectedControl.Prop_SecondaryColor2 = C;
            _SelectedControl.Invalidate();
            ((UI.Controllers.ColorItem)sender).BackColor = C;
            ((UI.Controllers.ColorItem)sender).Invalidate();

            CList.Clear();

        }

        private void GroupBox10_Click(object sender, EventArgs e)
        {

            if (e is DragEventArgs)
            {
                _SelectedControl.Prop_LoadingCircleBack1 = ((ColorItem)sender).BackColor;
                _SelectedControl.Invalidate();
                return;
            }

            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                Forms.SubMenu.ShowMenu((UI.Controllers.ColorItem)sender);
                if (ColorClipboard.Event == ColorClipboard.MenuEvent.Cut | ColorClipboard.Event == ColorClipboard.MenuEvent.Paste | ColorClipboard.Event == ColorClipboard.MenuEvent.Override)
                {
                    _SelectedControl.Prop_LoadingCircleBack1 = ((ColorItem)sender).BackColor;
                    _SelectedControl.Invalidate();
                }
                return;
            }

            List<Control> CList = new() { (UI.Controllers.ColorItem)sender, _SelectedControl };

            Conditions _conditions = new() { CursorCircle1 = true, Win7 = false, LivePreview_AfterGlow = false, LivePreview_Colorization = false };
            Color C = Forms.ColorPickerDlg.Pick(CList, _conditions, true);

            _SelectedControl.Prop_LoadingCircleBack1 = C;
            _SelectedControl.Invalidate();
            ((UI.Controllers.ColorItem)sender).BackColor = C;
            ((UI.Controllers.ColorItem)sender).Invalidate();

            CList.Clear();

        }

        private void CheckBox1_CheckedChanged(object sender)
        {
            if (!_Shown)
                return;
            _SelectedControl.Prop_PrimaryColorGradient = CheckBox1.Checked;
            _SelectedControl.Invalidate();
            CheckBox1.Invalidate();
        }

        private void CheckBox4_CheckedChanged(object sender)
        {
            if (!_Shown)
                return;

            _SelectedControl.Prop_SecondaryColorGradient = CheckBox4.Checked;
            _SelectedControl.Invalidate();
            CheckBox4.Invalidate();

        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_Shown)
                return;

            _SelectedControl.Prop_PrimaryColorGradientMode = Paths.ReturnGradientModeFromString(((UI.WP.ComboBox)sender).SelectedItem.ToString());
            _SelectedControl.Invalidate();
        }

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_Shown)
                return;

            _SelectedControl.Prop_SecondaryColorGradientMode = Paths.ReturnGradientModeFromString(((UI.WP.ComboBox)sender).SelectedItem.ToString());
            _SelectedControl.Invalidate();

        }

        private void CheckBox5_CheckedChanged(object sender)
        {
            if (!_Shown)
                return;

            _SelectedControl.Prop_PrimaryNoise = CheckBox5.Checked;
            _SelectedControl.Invalidate();
            CheckBox5.Invalidate();

        }

        private void CheckBox3_CheckedChanged(object sender)
        {
            if (!_Shown)
                return;

            _SelectedControl.Prop_SecondaryNoise = CheckBox3.Checked;
            _SelectedControl.Invalidate();
            CheckBox3.Invalidate();

        }

        private void Trackbar1_Scroll(object sender)
        {
            if (!_Shown)
                return;

            foreach (CursorControl i in FlowLayoutPanel1.Controls)
            {
                i.Prop_Scale = ((float)((UI.WP.Trackbar)sender).Value) / 100;
                i.Width = (int)Math.Round(32f * i.Prop_Scale + 32f);
                i.Height = i.Width;
                i.Refresh();
            }

            Label5.Text = $"{Program.Lang.Scaling} ({((float)((UI.WP.Trackbar)sender).Value) / 100}x)";
        }

        private float Angle = 180f;
        private readonly float Increment = 5f;
        private int Cycles = 0;

        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (!_Shown)
                return;

            try
            {
                foreach (CursorControl i in AnimateList)
                {
                    i.Angle = Angle;
                    i.Refresh();

                    if (Angle + Increment >= 360f)
                    {
                        Angle = 0f;
                    }

                    Angle += Increment;

                    if (Cycles >= 3)
                    {
                        i.Angle = 180f;
                        i.Refresh();

                        Timer1.Enabled = false;
                        Timer1.Stop();

                        Cycles = 0;
                    }
                    else
                    {
                        if (Angle == 180f) { Cycles += 1; }
                    }
                }
            }
            catch
            {
            }
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            AnimateList.Clear();

            foreach (CursorControl i in FlowLayoutPanel1.Controls.OfType<CursorControl>())
            {
                bool condition0 = !i.Prop_UseFromFile && (i.Prop_Cursor == Paths.CursorType.AppLoading | i.Prop_Cursor == Paths.CursorType.Busy);
                bool condition1 = i.Prop_UseFromFile && System.IO.File.Exists(i.Prop_File) && System.IO.Path.GetExtension(i.Prop_File).ToUpper() == ".ANI";
                if (condition0 || condition1) { AnimateList.Add(i); }
            }

            Angle = 180f;
            Cycles = 0;
            Timer1.Enabled = true;
            Timer1.Start();
        }

        private void GroupBox9_Click(object sender, EventArgs e)
        {

            if (e is DragEventArgs)
            {
                _SelectedControl.Prop_LoadingCircleBack1 = ((ColorItem)sender).BackColor;
                _SelectedControl.Invalidate();
                return;
            }

            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                Forms.SubMenu.ShowMenu((UI.Controllers.ColorItem)sender);
                if (ColorClipboard.Event == ColorClipboard.MenuEvent.Cut | ColorClipboard.Event == ColorClipboard.MenuEvent.Paste | ColorClipboard.Event == ColorClipboard.MenuEvent.Override)
                {
                    _SelectedControl.Prop_LoadingCircleBack1 = ((ColorItem)sender).BackColor;
                    _SelectedControl.Invalidate();
                }
                return;
            }

            List<Control> CList = new() { (UI.Controllers.ColorItem)sender, _SelectedControl };

            Conditions _conditions = new() { CursorCircle2 = true, Win7 = false, LivePreview_AfterGlow = false, LivePreview_Colorization = false };
            Color C = Forms.ColorPickerDlg.Pick(CList, _conditions, true);

            _SelectedControl.Prop_LoadingCircleBack2 = C;
            _SelectedControl.Invalidate();
            ((UI.Controllers.ColorItem)sender).BackColor = C;
            ((UI.Controllers.ColorItem)sender).Invalidate();

            CList.Clear();

        }

        private void GroupBox8_Click(object sender, EventArgs e)
        {

            if (e is DragEventArgs)
            {
                _SelectedControl.Prop_LoadingCircleHot1 = ((ColorItem)sender).BackColor;
                _SelectedControl.Invalidate();
                return;
            }

            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                Forms.SubMenu.ShowMenu((UI.Controllers.ColorItem)sender);
                if (ColorClipboard.Event == ColorClipboard.MenuEvent.Cut | ColorClipboard.Event == ColorClipboard.MenuEvent.Paste | ColorClipboard.Event == ColorClipboard.MenuEvent.Override)
                {
                    _SelectedControl.Prop_LoadingCircleHot1 = ((ColorItem)sender).BackColor;
                    _SelectedControl.Invalidate();
                }
                return;
            }

            List<Control> CList = new() { (UI.Controllers.ColorItem)sender, _SelectedControl };

            Conditions _conditions = new() { CursorCircleHot1 = true, Win7 = false, LivePreview_AfterGlow = false, LivePreview_Colorization = false };
            Color C = Forms.ColorPickerDlg.Pick(CList, _conditions, true);

            _SelectedControl.Prop_LoadingCircleHot1 = C;
            _SelectedControl.Invalidate();
            ((UI.Controllers.ColorItem)sender).BackColor = C;
            ((UI.Controllers.ColorItem)sender).Invalidate();

            CList.Clear();

        }

        private void GroupBox7_Click(object sender, EventArgs e)
        {

            if (e is DragEventArgs)
            {
                _SelectedControl.Prop_LoadingCircleHot2 = ((ColorItem)sender).BackColor;
                _SelectedControl.Invalidate();
                return;
            }

            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                Forms.SubMenu.ShowMenu((UI.Controllers.ColorItem)sender);
                if (ColorClipboard.Event == ColorClipboard.MenuEvent.Cut | ColorClipboard.Event == ColorClipboard.MenuEvent.Paste | ColorClipboard.Event == ColorClipboard.MenuEvent.Override)
                {
                    _SelectedControl.Prop_LoadingCircleHot2 = ((ColorItem)sender).BackColor;
                    _SelectedControl.Invalidate();
                }
                return;
            }

            List<Control> CList = new() { (UI.Controllers.ColorItem)sender, _SelectedControl };

            Conditions _conditions = new() { CursorCircleHot2 = true, Win7 = false, LivePreview_AfterGlow = false, LivePreview_Colorization = false };
            Color C = Forms.ColorPickerDlg.Pick(CList, _conditions, true);

            _SelectedControl.Prop_LoadingCircleHot2 = C;
            _SelectedControl.Invalidate();
            ((UI.Controllers.ColorItem)sender).BackColor = C;
            ((UI.Controllers.ColorItem)sender).Invalidate();

            CList.Clear();

        }

        private void CheckBox8_CheckedChanged(object sender)
        {
            if (!_Shown)
                return;

            _SelectedControl.Prop_LoadingCircleBackGradient = CheckBox8.Checked;
            _SelectedControl.Invalidate();
            CheckBox8.Invalidate();
        }

        private void CheckBox2_CheckedChanged(object sender)
        {
            if (!_Shown)
                return;

            _SelectedControl.Prop_LoadingCircleHotGradient = CheckBox2.Checked;
            _SelectedControl.Invalidate();
            CheckBox2.Invalidate();
        }

        private void CheckBox7_CheckedChanged(object sender)
        {
            if (!_Shown)
                return;

            _SelectedControl.Prop_LoadingCircleBackNoise = CheckBox7.Checked;
            _SelectedControl.Invalidate();
            CheckBox7.Invalidate();

        }

        private void CheckBox6_CheckedChanged(object sender)
        {
            if (!_Shown)
                return;

            _SelectedControl.Prop_LoadingCircleHotNoise = CheckBox6.Checked;
            _SelectedControl.Invalidate();
            CheckBox6.Invalidate();

        }

        private void ComboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_Shown)
                return;

            _SelectedControl.Prop_LoadingCircleBackGradientMode = Paths.ReturnGradientModeFromString(((UI.WP.ComboBox)sender).SelectedItem.ToString());
            _SelectedControl.Invalidate();

        }

        private void ComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_Shown)
                return;

            _SelectedControl.Prop_LoadingCircleHotGradientMode = Paths.ReturnGradientModeFromString(((UI.WP.ComboBox)sender).SelectedItem.ToString());
            _SelectedControl.Invalidate();

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            _CopiedControl = _SelectedControl;
            Button2.Enabled = true;
            Button6.Enabled = true;

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            ApplyColorsFromCursor(_CopiedControl);
            ApplyColorsToPreview(_SelectedControl);
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            foreach (CursorControl i in FlowLayoutPanel1.Controls)
            {
                if (i is CursorControl)
                {
                    ApplyColorsFromCursor(_CopiedControl);
                    ApplyColorsToPreview(i);
                    i.Invalidate();
                }
            }
        }


        private void Button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            SaveToTM(Program.TM);
            Close();
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            if (OpenFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Theme.Manager TMx = new(Theme.Manager.Source.File, OpenFileDialog1.FileName);
                LoadFromTM(TMx);
                TMx.Dispose();

                foreach (CursorControl x in FlowLayoutPanel1.Controls.OfType<CursorControl>())
                {
                    if (x.Focused)
                    {
                        ApplyColorsFromCursor(x);
                        break;
                    }
                }

            }
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            Theme.Manager TMx = new(Theme.Manager.Source.Registry);
            LoadFromTM(TMx);
            TMx.Dispose();

            foreach (CursorControl x in FlowLayoutPanel1.Controls.OfType<CursorControl>())
            {
                if (x.Focused)
                {
                    ApplyColorsFromCursor(x);
                    break;
                }
            }
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            using (Manager _Def = Theme.Default.Get(Program.PreviewStyle))
            {
                LoadFromTM(_Def);
            }

            foreach (CursorControl x in FlowLayoutPanel1.Controls.OfType<CursorControl>())
            {
                if (x.Focused)
                {
                    ApplyColorsFromCursor(x);
                    break;
                }
            }
        }

        private void Button10_Click(object sender, EventArgs e)
        {
            MsgBox(Program.Lang.ScalingTip, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Button11_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            Theme.Manager TMx = new(Theme.Manager.Source.Registry);
            SaveToTM(TMx);
            SaveToTM(Program.TM);
            TMx.Apply_Cursors();
            TMx.Win32.Broadcast_UPM_ToDefUsers();
            TMx.Dispose();
            Cursor = Cursors.Default;
        }

        private void Ttl_h_Click(object sender, EventArgs e)
        {
            string response = InputBox(Program.Lang.InputValue, ((UI.WP.Button)sender).Text, Program.Lang.ItMustBeNumerical);
            ((UI.WP.Button)sender).Text = Math.Max(Math.Min(Conversion.Val(response), Trackbar2.Maximum), Trackbar2.Minimum).ToString();
            Trackbar2.Value = (int)Math.Round(Conversion.Val(((UI.WP.Button)sender).Text));
        }

        private void Trackbar2_Scroll(object sender)
        {
            trails_btn.Text = ((UI.WP.Trackbar)sender).Value.ToString();
        }

        private void Toggle1_CheckedChanged(object sender, EventArgs e)
        {
            checker_img.Image = ((UI.WP.Toggle)sender).Checked ? Properties.Resources.checker_enabled : Properties.Resources.checker_disabled;
        }

        private void ComboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_Shown)
                return;

            _SelectedControl.Prop_ArrowStyle = (Paths.ArrowStyle)ComboBox5.SelectedIndex;
            _SelectedControl.Invalidate();
        }

        private void ComboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_Shown)
                return;

            _SelectedControl.Prop_CircleStyle = (Paths.CircleStyle)ComboBox6.SelectedIndex;
            _SelectedControl.Invalidate();
        }

        private void Button12_Click(object sender, EventArgs e)
        {
            string response = InputBox(Program.Lang.InputValue, ((UI.WP.Button)sender).Text, Program.Lang.ItMustBeNumerical);
            ((UI.WP.Button)sender).Text = Math.Max(Math.Min(Conversion.Val(response), Trackbar3.Maximum), Trackbar3.Minimum).ToString();
            Trackbar3.Value = (int)Math.Round(Conversion.Val(((UI.WP.Button)sender).Text));
        }

        private void Trackbar3_Scroll(object sender)
        {
            Button12.Text = ((UI.WP.Trackbar)sender).Value.ToString();

            if (!_Shown)
                return;

            float valX = Conversions.ToSingle(((UI.WP.Trackbar)sender).Value);
            if (valX > 100f)
            {
                valX = 100f;
            }
            else if (valX < 0f)
            {
                valX = 0f;
            }

            _SelectedControl.Prop_PrimaryNoiseOpacity = valX / 100f;
            _SelectedControl.Invalidate();
        }

        private void Button13_Click(object sender, EventArgs e)
        {
            string response = InputBox(Program.Lang.InputValue, ((UI.WP.Button)sender).Text, Program.Lang.ItMustBeNumerical);
            ((UI.WP.Button)sender).Text = Math.Max(Math.Min(Conversion.Val(response), Trackbar4.Maximum), Trackbar4.Minimum).ToString();
            Trackbar4.Value = (int)Math.Round(Conversion.Val(((UI.WP.Button)sender).Text));
        }

        private void Trackbar4_Scroll(object sender)
        {
            Button13.Text = ((UI.WP.Trackbar)sender).Value.ToString();

            if (!_Shown)
                return;

            float valX = Conversions.ToSingle(((UI.WP.Trackbar)sender).Value);
            if (valX > 100f)
            {
                valX = 100f;
            }
            else if (valX < 0f)
            {
                valX = 0f;
            }

            _SelectedControl.Prop_SecondaryNoiseOpacity = valX / 100f;
            _SelectedControl.Invalidate();
        }

        private void Button14_Click(object sender, EventArgs e)
        {
            string response = InputBox(Program.Lang.InputValue, ((UI.WP.Button)sender).Text, Program.Lang.ItMustBeNumerical);
            ((UI.WP.Button)sender).Text = Math.Max(Math.Min(Conversion.Val(response), Trackbar5.Maximum), Trackbar5.Minimum).ToString();
            Trackbar5.Value = (int)Math.Round(Conversion.Val(((UI.WP.Button)sender).Text));
        }

        private void Trackbar5_Scroll(object sender)
        {
            Button14.Text = ((UI.WP.Trackbar)sender).Value.ToString();

            if (!_Shown)
                return;

            float valX = Conversions.ToSingle(((UI.WP.Trackbar)sender).Value);
            if (valX > 100f)
            {
                valX = 100f;
            }
            else if (valX < 0f)
            {
                valX = 0f;
            }

            _SelectedControl.Prop_LoadingCircleBackNoiseOpacity = valX / 100f;
            _SelectedControl.Invalidate();
        }

        private void Button15_Click(object sender, EventArgs e)
        {
            string response = InputBox(Program.Lang.InputValue, ((UI.WP.Button)sender).Text, Program.Lang.ItMustBeNumerical);
            ((UI.WP.Button)sender).Text = Math.Max(Math.Min(Conversion.Val(response), Trackbar6.Maximum), Trackbar6.Minimum).ToString();
            Trackbar6.Value = (int)Math.Round(Conversion.Val(((UI.WP.Button)sender).Text));
        }

        private void Trackbar6_Scroll(object sender)
        {
            Button15.Text = ((UI.WP.Trackbar)sender).Value.ToString();

            if (!_Shown)
                return;

            float valX = Conversions.ToSingle(((UI.WP.Trackbar)sender).Value);
            if (valX > 100f)
            {
                valX = 100f;
            }
            else if (valX < 0f)
            {
                valX = 0f;
            }

            _SelectedControl.Prop_LoadingCircleHotNoiseOpacity = valX / 100f;
            _SelectedControl.Invalidate();
        }

        private void ColorItem1_Click(object sender, EventArgs e)
        {

            if (e is DragEventArgs)
            {
                _SelectedControl.Prop_Shadow_Color = ((ColorItem)sender).BackColor;
                _SelectedControl.Invalidate();
                return;
            }

            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                Forms.SubMenu.ShowMenu((UI.Controllers.ColorItem)sender);
                if (ColorClipboard.Event == ColorClipboard.MenuEvent.Cut | ColorClipboard.Event == ColorClipboard.MenuEvent.Paste | ColorClipboard.Event == ColorClipboard.MenuEvent.Override)
                {
                    _SelectedControl.Prop_Shadow_Color = ((ColorItem)sender).BackColor;
                    _SelectedControl.Invalidate();
                }
                return;
            }

            List<Control> CList = new() { (UI.Controllers.ColorItem)sender, _SelectedControl };

            Conditions _conditions = new() { CursorShadow = true };
            Color C = Forms.ColorPickerDlg.Pick(CList, _conditions);

            _SelectedControl.Prop_Shadow_Color = C;
            _SelectedControl.Invalidate();
            ((UI.Controllers.ColorItem)sender).BackColor = C;
            ((UI.Controllers.ColorItem)sender).Invalidate();

            CList.Clear();

        }

        private void Button16_Click(object sender, EventArgs e)
        {
            string response = InputBox(Program.Lang.InputValue, ((UI.WP.Button)sender).Text, Program.Lang.ItMustBeNumerical);
            ((UI.WP.Button)sender).Text = Math.Max(Math.Min(Conversion.Val(response), Trackbar7.Maximum), Trackbar7.Minimum).ToString();
            Trackbar7.Value = (int)Math.Round(Conversion.Val(((UI.WP.Button)sender).Text));
        }

        private void Button17_Click(object sender, EventArgs e)
        {
            string response = InputBox(Program.Lang.InputValue, ((UI.WP.Button)sender).Text, Program.Lang.ItMustBeNumerical);
            ((UI.WP.Button)sender).Text = Math.Max(Math.Min(Conversion.Val(response), Trackbar8.Maximum), Trackbar8.Minimum).ToString();
            Trackbar8.Value = (int)Math.Round(Conversion.Val(((UI.WP.Button)sender).Text));
        }

        private void Button18_Click(object sender, EventArgs e)
        {
            string response = InputBox(Program.Lang.InputValue, ((UI.WP.Button)sender).Text, Program.Lang.ItMustBeNumerical);
            ((UI.WP.Button)sender).Text = Math.Max(Math.Min(Conversion.Val(response), Trackbar9.Maximum), Trackbar9.Minimum).ToString();
            Trackbar9.Value = (int)Math.Round(Conversion.Val(((UI.WP.Button)sender).Text));
        }

        private void Button19_Click(object sender, EventArgs e)
        {
            string response = InputBox(Program.Lang.InputValue, ((UI.WP.Button)sender).Text, Program.Lang.ItMustBeNumerical);
            ((UI.WP.Button)sender).Text = Math.Max(Math.Min(Conversion.Val(response), Trackbar10.Maximum), Trackbar10.Minimum).ToString();
            Trackbar10.Value = (int)Math.Round(Conversion.Val(((UI.WP.Button)sender).Text));
        }

        private void Trackbar7_Scroll(object sender)
        {
            Button16.Text = ((UI.WP.Trackbar)sender).Value.ToString();

            if (!_Shown)
                return;

            float valX = Conversions.ToSingle(((UI.WP.Trackbar)sender).Value);
            if (valX > 10f)
            {
                valX = 10f;
            }
            else if (valX < 0f)
            {
                valX = 0f;
            }

            _SelectedControl.Prop_Shadow_Blur = (int)Math.Round(valX);
            _SelectedControl.Invalidate();
        }

        private void Trackbar8_Scroll(object sender)
        {
            Button17.Text = ((UI.WP.Trackbar)sender).Value.ToString();

            if (!_Shown)
                return;

            float valX = Conversions.ToSingle(((UI.WP.Trackbar)sender).Value);
            if (valX > 100f)
            {
                valX = 100f;
            }
            else if (valX < 0f)
            {
                valX = 0f;
            }

            _SelectedControl.Prop_Shadow_Opacity = valX / 100f;
            _SelectedControl.Invalidate();
        }

        private void Trackbar9_Scroll(object sender)
        {
            Button18.Text = ((UI.WP.Trackbar)sender).Value.ToString();

            if (!_Shown)
                return;

            float valX = Conversions.ToSingle(((UI.WP.Trackbar)sender).Value);
            if (valX > 5f)
            {
                valX = 5f;
            }
            else if (valX < 0f)
            {
                valX = 0f;
            }

            _SelectedControl.Prop_Shadow_OffsetX = (int)Math.Round(valX);
            _SelectedControl.Invalidate();
        }

        private void Trackbar10_Scroll(object sender)
        {
            Button19.Text = ((UI.WP.Trackbar)sender).Value.ToString();

            if (!_Shown)
                return;

            float valX = Conversions.ToSingle(((UI.WP.Trackbar)sender).Value);
            if (valX > 5f)
            {
                valX = 5f;
            }
            else if (valX < 0f)
            {
                valX = 0f;
            }

            _SelectedControl.Prop_Shadow_OffsetY = (int)Math.Round(valX);
            _SelectedControl.Invalidate();
        }

        private void CheckBox11_CheckedChanged(object sender)
        {
            if (!_Shown)
                return;
            _SelectedControl.Prop_Shadow_Enabled = CheckBox11.Checked;
            _SelectedControl.Invalidate();
        }

        private void CursorsStudio_HelpButtonClicked(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Process.Start($"{Properties.Resources.Link_Wiki}/Edit-Windows-cursors");
        }

        private void button20_Click(object sender, EventArgs e)
        {
            if (openFileDialog2.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = openFileDialog2.FileName;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            _SelectedControl.Prop_File = textBox1.Text;
            _SelectedControl.Invalidate();
        }

        private void source0_CheckedChanged(object sender)
        {
            _SelectedControl.Prop_UseFromFile = !source0.Checked;
            _SelectedControl.Invalidate();

            if (source0.Checked)
            {
                ComboBox5.Enabled = true; ComboBox6.Enabled = true;
                tabControl1.TabPages[1].GetAllControls().ToList().ForEach(x => x.Enabled = true);
                tabControl1.TabPages[2].GetAllControls().ToList().ForEach(x => x.Enabled = true);
                tabControl1.TabPages[3].GetAllControls().ToList().ForEach(x => x.Enabled = false);
            }
        }

        private void source1_CheckedChanged(object sender)
        {
            _SelectedControl.Prop_UseFromFile = source1.Checked;
            _SelectedControl.Invalidate();

            if (source1.Checked)
            {
                ComboBox5.Enabled = false; ComboBox6.Enabled = false;
                tabControl1.TabPages[1].GetAllControls().ToList().ForEach(x => x.Enabled = false);
                tabControl1.TabPages[2].GetAllControls().ToList().ForEach(x => x.Enabled = false);
                tabControl1.TabPages[3].GetAllControls().ToList().ForEach(x => x.Enabled = true);
            }
        }

        private void GroupBox8_Click(object sender, DragEventArgs e)
        {

        }

        private void GroupBox10_Click(object sender, DragEventArgs e)
        {

        }

        private void GroupBox7_Click(object sender, DragEventArgs e)
        {

        }

        private void GroupBox9_Click(object sender, DragEventArgs e)
        {

        }

        private void GroupBox5_Click(object sender, DragEventArgs e)
        {

        }

        private void GroupBox4_Click(object sender, DragEventArgs e)
        {

        }

        private void GroupBox3_Click(object sender, DragEventArgs e)
        {

        }

        private void TaskbarFrontAndFoldersOnStart_picker_Click(object sender, DragEventArgs e)
        {

        }

        private void ColorItem1_Click(object sender, DragEventArgs e)
        {

        }
    }
}
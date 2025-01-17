﻿using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using static WinPaletter.PreviewHelpers;

namespace WinPaletter
{

    public partial class WindowsTerminal
    {
        private bool _Shown = false;
        public WinTerminal.Version _Mode;
        public WinTerminal _Terminal;
        public WinTerminal _TerminalDefault;

        public WindowsTerminal()
        {
            SaveState = _Mode;
            InitializeComponent();
        }

        private void WindowsTerminal_Load(object sender, EventArgs e)
        {
            CheckBox1.Checked = Program.Settings.WindowsTerminals.ListAllFonts;

            this.LoadLanguage();
            ApplyStyle(this);
            _Shown = false;

            switch (_Mode)
            {
                case WinTerminal.Version.Stable:
                    {
                        _Terminal = Program.TM.Terminal;
                        _TerminalDefault = Program.TM.Terminal;
                        Text = Program.Lang.TerminalStable;
                        TerEnabled.Checked = Program.TM.Terminal.Enabled;
                        break;
                    }

                case WinTerminal.Version.Preview:
                    {
                        _Terminal = Program.TM.TerminalPreview;
                        _TerminalDefault = Program.TM.TerminalPreview;

                        Text = Program.Lang.TerminalPreview;
                        TerEnabled.Checked = Program.TM.TerminalPreview.Enabled;
                        break;
                    }

            }

            Load_FromTerminal();

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

        public void Load_FromTerminal()
        {

            FillTerminalSchemes(_Terminal, TerSchemes);
            FillTerminalThemes(_Terminal, TerThemes);
            FillTerminalProfiles(_Terminal, TerProfiles);

            TerProfiles.SelectedIndex = 0;

            Terminal1.PreviewVersion = _Mode == WinTerminal.Version.Preview;
            Terminal2.PreviewVersion = _Mode == WinTerminal.Version.Preview;

            if (_Terminal.Theme.ToLower() == "dark")
            {
                TerThemes.SelectedIndex = 0;
                TerTitlebarActive.BackColor = default;
                TerTitlebarInactive.BackColor = default;
                TerTabActive.BackColor = default;
                TerTabInactive.BackColor = default;
                TerMode.Checked = true;
                Terminal1.Light = false;
                Terminal2.Light = false;
            }

            else if (_Terminal.Theme.ToLower() == "light")
            {
                TerThemes.SelectedIndex = 1;
                TerTitlebarActive.BackColor = default;
                TerTitlebarInactive.BackColor = default;
                TerTabActive.BackColor = default;
                TerTabInactive.BackColor = default;
                TerMode.Checked = false;
                Terminal1.Light = true;
                Terminal2.Light = true;
            }

            else if (_Terminal.Theme.ToLower() == "system")
            {
                TerThemes.SelectedIndex = 2;
                TerTitlebarActive.BackColor = default;
                TerTitlebarInactive.BackColor = default;
                TerTabActive.BackColor = default;
                TerTabInactive.BackColor = default;

                switch (Program.PreviewStyle)
                {
                    case WindowStyle.W12:
                        {
                            TerMode.Checked = !Program.TM.Windows12.AppMode_Light;
                            Terminal1.Light = Program.TM.Windows12.AppMode_Light;
                            Terminal2.Light = Program.TM.Windows12.AppMode_Light;
                            break;
                        }

                    case WindowStyle.W11:
                        {
                            TerMode.Checked = !Program.TM.Windows11.AppMode_Light;
                            Terminal1.Light = Program.TM.Windows11.AppMode_Light;
                            Terminal2.Light = Program.TM.Windows11.AppMode_Light;
                            break;
                        }

                    case WindowStyle.W10:
                        {
                            TerMode.Checked = !Program.TM.Windows10.AppMode_Light;
                            Terminal1.Light = Program.TM.Windows10.AppMode_Light;
                            Terminal2.Light = Program.TM.Windows10.AppMode_Light;
                            break;
                        }

                    default:
                        {
                            TerMode.Checked = !Program.TM.Windows12.AppMode_Light;
                            Terminal1.Light = Program.TM.Windows12.AppMode_Light;
                            Terminal2.Light = Program.TM.Windows12.AppMode_Light;
                            break;
                        }
                }
            }


            else if (TerThemes.Items.Contains(_Terminal.Theme))
            {

                TerThemes.SelectedItem = _Terminal.Theme;

                TerThemesContainer.Enabled = true;

                {
                    TTheme temp = _Terminal.Themes[TerThemes.SelectedIndex - 3];
                    TerTitlebarActive.BackColor = temp.Titlebar_Active;
                    TerTitlebarInactive.BackColor = temp.Titlebar_Inactive;
                    TerTabActive.BackColor = temp.Tab_Active;
                    TerTabInactive.BackColor = temp.Tab_Inactive;
                    TerMode.Checked = !(temp.Style.ToLower() == "light");
                    Terminal1.Light = !(temp.Style.ToLower() == "light");
                    Terminal2.Light = !(temp.Style.ToLower() == "light");
                }

            }

            ApplyPreview(_Terminal);
        }

        private void WindowsTerminal_Shown(object sender, EventArgs e)
        {
            _Shown = true;
        }

        public void FillTerminalSchemes(WinTerminal Terminal, ComboBox Combobox)
        {
            Combobox.Items.Clear();

            if (Terminal.Colors.Count > 0)
            {
                for (int x = 0, loopTo = Terminal.Colors.Count - 1; x <= loopTo; x++)
                    Combobox.Items.Add(Terminal.Colors[x].Name);
            }

        }

        public void FillTerminalThemes(WinTerminal Terminal, ComboBox Combobox)
        {
            Combobox.Items.Clear();

            Combobox.Items.Add("Dark");
            Combobox.Items.Add("Light");
            Combobox.Items.Add("System");

            if (Terminal.Themes.Count > 0)
            {
                for (int x = 0, loopTo = Terminal.Themes.Count - 1; x <= loopTo; x++)
                    Combobox.Items.Add(Terminal.Themes[x].Name);
            }

        }

        public void FillTerminalProfiles(WinTerminal Terminal, ComboBox Combobox)
        {
            Combobox.Items.Clear();
            Combobox.Items.Add("Default");

            if (Terminal.Profiles.Count > 0)
            {
                for (int x = 0, loopTo = Terminal.Profiles.Count - 1; x <= loopTo; x++)
                    Combobox.Items.Add(Terminal.Profiles[x].Name);
            }

        }

        private void TerSchemes_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetDefaultsToScheme(TerSchemes.SelectedItem.ToString());

            try
            {
                {
                    TColors temp = _Terminal.Colors[TerSchemes.SelectedIndex];
                    TerBackground.BackColor = temp.Background;
                    TerForeground.BackColor = temp.Foreground;
                    TerSelection.BackColor = temp.SelectionBackground;
                    TerCursor.BackColor = temp.CursorColor;

                    TerBlack.BackColor = temp.Black;
                    TerBlue.BackColor = temp.Blue;
                    TerGreen.BackColor = temp.Green;
                    TerCyan.BackColor = temp.Cyan;
                    TerRed.BackColor = temp.Red;
                    TerPurple.BackColor = temp.Purple;
                    TerYellow.BackColor = temp.Yellow;
                    TerWhite.BackColor = temp.White;

                    TerBlackB.BackColor = temp.BrightBlack;
                    TerBlueB.BackColor = temp.BrightBlue;
                    TerGreenB.BackColor = temp.BrightGreen;
                    TerCyanB.BackColor = temp.BrightCyan;
                    TerRedB.BackColor = temp.BrightRed;
                    TerPurpleB.BackColor = temp.BrightPurple;
                    TerYellowB.BackColor = temp.BrightYellow;
                    TerWhiteB.BackColor = temp.BrightWhite;
                }

                {
                    TProfile temp1 = TerProfiles.SelectedIndex == 0 ? _Terminal.DefaultProf : _Terminal.Profiles[TerProfiles.SelectedIndex - 1];
                    temp1.ColorScheme = TerSchemes.SelectedItem.ToString();
                }

                if (_Shown)
                    ApplyPreview(_Terminal);
            }
            catch
            {

            }
        }

        public void SetDefaultsToScheme(string Scheme)
        {
            switch (Scheme.ToLower() ?? string.Empty)
            {

                case var @case when @case == ("Campbell".ToLower() ?? string.Empty):
                    {
                        TerBackground.DefaultBackColor = "FF0C0C0C".FromHEXToColor(true);
                        TerBlack.DefaultBackColor = "FF0C0C0C".FromHEXToColor(true);
                        TerBlue.DefaultBackColor = "FF0037DA".FromHEXToColor(true);
                        TerBlackB.DefaultBackColor = "FF767676".FromHEXToColor(true);
                        TerBlueB.DefaultBackColor = "FF3B78FF".FromHEXToColor(true);
                        TerCyanB.DefaultBackColor = "FF61D6D6".FromHEXToColor(true);
                        TerGreenB.DefaultBackColor = "FF16C60C".FromHEXToColor(true);
                        TerPurpleB.DefaultBackColor = "FFB4009E".FromHEXToColor(true);
                        TerRedB.DefaultBackColor = "FFE74856".FromHEXToColor(true);
                        TerWhiteB.DefaultBackColor = "FFF2F2F2".FromHEXToColor(true);
                        TerYellowB.DefaultBackColor = "FFF9F1A5".FromHEXToColor(true);
                        TerCursor.DefaultBackColor = "FFFFFFFF".FromHEXToColor(true);
                        TerCyan.DefaultBackColor = "FF3A96DD".FromHEXToColor(true);
                        TerForeground.DefaultBackColor = "FFCCCCCC".FromHEXToColor(true);
                        TerGreen.DefaultBackColor = "FF13A10E".FromHEXToColor(true);
                        TerPurple.DefaultBackColor = "FF881798".FromHEXToColor(true);
                        TerRed.DefaultBackColor = "FFC50F1F".FromHEXToColor(true);
                        TerSelection.DefaultBackColor = "FFFFFFFF".FromHEXToColor(true);
                        TerWhite.DefaultBackColor = "FFCCCCCC".FromHEXToColor(true);
                        TerYellow.DefaultBackColor = "FFC19C00".FromHEXToColor(true);
                        break;
                    }

                case var case1 when case1 == ("Campbell Powershell".ToLower() ?? string.Empty):
                    {
                        TerBackground.DefaultBackColor = "FF012456".FromHEXToColor(true);
                        TerBlack.DefaultBackColor = "FF0C0C0C".FromHEXToColor(true);
                        TerBlue.DefaultBackColor = "FF0037DA".FromHEXToColor(true);
                        TerBlackB.DefaultBackColor = "FF767676".FromHEXToColor(true);
                        TerBlueB.DefaultBackColor = "FF3B78FF".FromHEXToColor(true);
                        TerCyanB.DefaultBackColor = "FF61D6D6".FromHEXToColor(true);
                        TerGreenB.DefaultBackColor = "FF16C60C".FromHEXToColor(true);
                        TerPurpleB.DefaultBackColor = "FFB4009E".FromHEXToColor(true);
                        TerRedB.DefaultBackColor = "FFE74856".FromHEXToColor(true);
                        TerWhiteB.DefaultBackColor = "FFF2F2F2".FromHEXToColor(true);
                        TerYellowB.DefaultBackColor = "FFF9F1A5".FromHEXToColor(true);
                        TerCursor.DefaultBackColor = "FFFFFFFF".FromHEXToColor(true);
                        TerCyan.DefaultBackColor = "FF3A96DD".FromHEXToColor(true);
                        TerForeground.DefaultBackColor = "FFCCCCCC".FromHEXToColor(true);
                        TerGreen.DefaultBackColor = "FF13A10E".FromHEXToColor(true);
                        TerPurple.DefaultBackColor = "FF881798".FromHEXToColor(true);
                        TerRed.DefaultBackColor = "FFC50F1F".FromHEXToColor(true);
                        TerSelection.DefaultBackColor = "FFFFFFFF".FromHEXToColor(true);
                        TerWhite.DefaultBackColor = "FFCCCCCC".FromHEXToColor(true);
                        TerYellow.DefaultBackColor = "FFC19C00".FromHEXToColor(true);
                        break;
                    }

                case var case2 when case2 == ("One Half Dark".ToLower() ?? string.Empty):
                    {
                        TerBackground.DefaultBackColor = "FF282C34".FromHEXToColor(true);
                        TerBlack.DefaultBackColor = "FF282C34".FromHEXToColor(true);
                        TerBlue.DefaultBackColor = "FF61AFEF".FromHEXToColor(true);
                        TerBlackB.DefaultBackColor = "FF5A6374".FromHEXToColor(true);
                        TerBlueB.DefaultBackColor = "FF61AFEF".FromHEXToColor(true);
                        TerCyanB.DefaultBackColor = "FF56B6C2".FromHEXToColor(true);
                        TerGreenB.DefaultBackColor = "FF98C379".FromHEXToColor(true);
                        TerPurpleB.DefaultBackColor = "FFC678DD".FromHEXToColor(true);
                        TerRedB.DefaultBackColor = "FFE06C75".FromHEXToColor(true);
                        TerWhiteB.DefaultBackColor = "FFDCDFE4".FromHEXToColor(true);
                        TerYellowB.DefaultBackColor = "FFE5C07B".FromHEXToColor(true);
                        TerCursor.DefaultBackColor = "FFFFFFFF".FromHEXToColor(true);
                        TerCyan.DefaultBackColor = "FF56B6C2".FromHEXToColor(true);
                        TerForeground.DefaultBackColor = "FFDCDFE4".FromHEXToColor(true);
                        TerGreen.DefaultBackColor = "FF98C379".FromHEXToColor(true);
                        TerPurple.DefaultBackColor = "FFC678DD".FromHEXToColor(true);
                        TerRed.DefaultBackColor = "FFE06C75".FromHEXToColor(true);
                        TerSelection.DefaultBackColor = "FFFFFFFF".FromHEXToColor(true);
                        TerWhite.DefaultBackColor = "FFDCDFE4".FromHEXToColor(true);
                        TerYellow.DefaultBackColor = "FFE5C07B".FromHEXToColor(true);
                        break;
                    }

                case var case3 when case3 == ("One Half Light".ToLower() ?? string.Empty):
                    {
                        TerBackground.DefaultBackColor = "FFFAFAFA".FromHEXToColor(true);
                        TerBlack.DefaultBackColor = "FF383A42".FromHEXToColor(true);
                        TerBlue.DefaultBackColor = "FF0184BC".FromHEXToColor(true);
                        TerBlackB.DefaultBackColor = "FF4F525D".FromHEXToColor(true);
                        TerBlueB.DefaultBackColor = "FF61AFEF".FromHEXToColor(true);
                        TerCyanB.DefaultBackColor = "FF56B5C1".FromHEXToColor(true);
                        TerGreenB.DefaultBackColor = "FF98C379".FromHEXToColor(true);
                        TerPurpleB.DefaultBackColor = "FFC577DD".FromHEXToColor(true);
                        TerRedB.DefaultBackColor = "FFDF6C75".FromHEXToColor(true);
                        TerWhiteB.DefaultBackColor = "FFFFFFFF".FromHEXToColor(true);
                        TerYellowB.DefaultBackColor = "FFE4C07A".FromHEXToColor(true);
                        TerCursor.DefaultBackColor = "FF4F525D".FromHEXToColor(true);
                        TerCyan.DefaultBackColor = "FF0997B3".FromHEXToColor(true);
                        TerForeground.DefaultBackColor = "FF383A42".FromHEXToColor(true);
                        TerGreen.DefaultBackColor = "FF50A14F".FromHEXToColor(true);
                        TerPurple.DefaultBackColor = "FFA626A4".FromHEXToColor(true);
                        TerRed.DefaultBackColor = "FFE45649".FromHEXToColor(true);
                        TerSelection.DefaultBackColor = "FFFFFFFF".FromHEXToColor(true);
                        TerWhite.DefaultBackColor = "FFFAFAFA".FromHEXToColor(true);
                        TerYellow.DefaultBackColor = "FFC18301".FromHEXToColor(true);
                        break;
                    }

                case var case4 when case4 == ("Solarized Dark".ToLower() ?? string.Empty):
                    {
                        TerBackground.DefaultBackColor = "FF002B36".FromHEXToColor(true);
                        TerBlack.DefaultBackColor = "FF002B36".FromHEXToColor(true);
                        TerBlue.DefaultBackColor = "FF268BD2".FromHEXToColor(true);
                        TerBlackB.DefaultBackColor = "FF073642".FromHEXToColor(true);
                        TerBlueB.DefaultBackColor = "FF839496".FromHEXToColor(true);
                        TerCyanB.DefaultBackColor = "FF93A1A1".FromHEXToColor(true);
                        TerGreenB.DefaultBackColor = "FF586E75".FromHEXToColor(true);
                        TerPurpleB.DefaultBackColor = "FF6C71C4".FromHEXToColor(true);
                        TerRedB.DefaultBackColor = "FFCB4B16".FromHEXToColor(true);
                        TerWhiteB.DefaultBackColor = "FFFDF6E3".FromHEXToColor(true);
                        TerYellowB.DefaultBackColor = "FF657B83".FromHEXToColor(true);
                        TerCursor.DefaultBackColor = "FFFFFFFF".FromHEXToColor(true);
                        TerCyan.DefaultBackColor = "FF2AA198".FromHEXToColor(true);
                        TerForeground.DefaultBackColor = "FF839496".FromHEXToColor(true);
                        TerGreen.DefaultBackColor = "FF859900".FromHEXToColor(true);
                        TerPurple.DefaultBackColor = "FFD33682".FromHEXToColor(true);
                        TerRed.DefaultBackColor = "FFDC322F".FromHEXToColor(true);
                        TerSelection.DefaultBackColor = "FFFFFFFF".FromHEXToColor(true);
                        TerWhite.DefaultBackColor = "FFEEE8D5".FromHEXToColor(true);
                        TerYellow.DefaultBackColor = "FFB58900".FromHEXToColor(true);
                        break;
                    }

                case var case5 when case5 == ("Solarized Light".ToLower() ?? string.Empty):
                    {
                        TerBackground.DefaultBackColor = "FFFDF6E3".FromHEXToColor(true);
                        TerBlack.DefaultBackColor = "FF002B36".FromHEXToColor(true);
                        TerBlue.DefaultBackColor = "FF268BD2".FromHEXToColor(true);
                        TerBlackB.DefaultBackColor = "FF073642".FromHEXToColor(true);
                        TerBlueB.DefaultBackColor = "FF839496".FromHEXToColor(true);
                        TerCyanB.DefaultBackColor = "FF93A1A1".FromHEXToColor(true);
                        TerGreenB.DefaultBackColor = "FF586E75".FromHEXToColor(true);
                        TerPurpleB.DefaultBackColor = "FF6C71C4".FromHEXToColor(true);
                        TerRedB.DefaultBackColor = "FFCB4B16".FromHEXToColor(true);
                        TerWhiteB.DefaultBackColor = "FFFDF6E3".FromHEXToColor(true);
                        TerYellowB.DefaultBackColor = "FF657B83".FromHEXToColor(true);
                        TerCursor.DefaultBackColor = "FF002B36".FromHEXToColor(true);
                        TerCyan.DefaultBackColor = "FF2AA198".FromHEXToColor(true);
                        TerForeground.DefaultBackColor = "FF657B83".FromHEXToColor(true);
                        TerGreen.DefaultBackColor = "FF859900".FromHEXToColor(true);
                        TerPurple.DefaultBackColor = "FFD33682".FromHEXToColor(true);
                        TerRed.DefaultBackColor = "FFDC322F".FromHEXToColor(true);
                        TerSelection.DefaultBackColor = "FFFFFFFF".FromHEXToColor(true);
                        TerWhite.DefaultBackColor = "FFEEE8D5".FromHEXToColor(true);
                        TerYellow.DefaultBackColor = "FFB58900".FromHEXToColor(true);
                        break;
                    }

                case var case6 when case6 == ("Tango Dark".ToLower() ?? string.Empty):
                    {
                        TerBackground.DefaultBackColor = "FF000000".FromHEXToColor(true);
                        TerBlack.DefaultBackColor = "FF000000".FromHEXToColor(true);
                        TerBlue.DefaultBackColor = "FF3465A4".FromHEXToColor(true);
                        TerBlackB.DefaultBackColor = "FF555753".FromHEXToColor(true);
                        TerBlueB.DefaultBackColor = "FF729FCF".FromHEXToColor(true);
                        TerCyanB.DefaultBackColor = "FF34E2E2".FromHEXToColor(true);
                        TerGreenB.DefaultBackColor = "FF8AE234".FromHEXToColor(true);
                        TerPurpleB.DefaultBackColor = "FFAD7FA8".FromHEXToColor(true);
                        TerRedB.DefaultBackColor = "FFEF2929".FromHEXToColor(true);
                        TerWhiteB.DefaultBackColor = "FFEEEEEC".FromHEXToColor(true);
                        TerYellowB.DefaultBackColor = "FFFCE94F".FromHEXToColor(true);
                        TerCursor.DefaultBackColor = "FFFFFFFF".FromHEXToColor(true);
                        TerCyan.DefaultBackColor = "FF06989A".FromHEXToColor(true);
                        TerForeground.DefaultBackColor = "FFD3D7CF".FromHEXToColor(true);
                        TerGreen.DefaultBackColor = "FF4E9A06".FromHEXToColor(true);
                        TerPurple.DefaultBackColor = "FF75507B".FromHEXToColor(true);
                        TerRed.DefaultBackColor = "FFCC0000".FromHEXToColor(true);
                        TerSelection.DefaultBackColor = "FFFFFFFF".FromHEXToColor(true);
                        TerWhite.DefaultBackColor = "FFD3D7CF".FromHEXToColor(true);
                        TerYellow.DefaultBackColor = "FFC4A000".FromHEXToColor(true);
                        break;
                    }

                case var case7 when case7 == ("Tango Light".ToLower() ?? string.Empty):
                    {
                        TerBackground.DefaultBackColor = "FFFFFFFF".FromHEXToColor(true);
                        TerBlack.DefaultBackColor = "FF000000".FromHEXToColor(true);
                        TerBlue.DefaultBackColor = "FF3465A4".FromHEXToColor(true);
                        TerBlackB.DefaultBackColor = "FF555753".FromHEXToColor(true);
                        TerBlueB.DefaultBackColor = "FF729FCF".FromHEXToColor(true);
                        TerCyanB.DefaultBackColor = "FF34E2E2".FromHEXToColor(true);
                        TerGreenB.DefaultBackColor = "FF8AE234".FromHEXToColor(true);
                        TerPurpleB.DefaultBackColor = "FFAD7FA8".FromHEXToColor(true);
                        TerRedB.DefaultBackColor = "FFEF2929".FromHEXToColor(true);
                        TerWhiteB.DefaultBackColor = "FFEEEEEC".FromHEXToColor(true);
                        TerYellowB.DefaultBackColor = "FFFCE94F".FromHEXToColor(true);
                        TerCursor.DefaultBackColor = "FF000000".FromHEXToColor(true);
                        TerCyan.DefaultBackColor = "FF06989A".FromHEXToColor(true);
                        TerForeground.DefaultBackColor = "FF555753".FromHEXToColor(true);
                        TerGreen.DefaultBackColor = "FF4E9A06".FromHEXToColor(true);
                        TerPurple.DefaultBackColor = "FF75507B".FromHEXToColor(true);
                        TerRed.DefaultBackColor = "FFCC0000".FromHEXToColor(true);
                        TerSelection.DefaultBackColor = "FFFFFFFF".FromHEXToColor(true);
                        TerWhite.DefaultBackColor = "FFD3D7CF".FromHEXToColor(true);
                        TerYellow.DefaultBackColor = "FFC4A000".FromHEXToColor(true);
                        break;
                    }

                case var case8 when case8 == ("Vintage".ToLower() ?? string.Empty):
                    {
                        TerBackground.DefaultBackColor = "FF000000".FromHEXToColor(true);
                        TerBlack.DefaultBackColor = "FF000000".FromHEXToColor(true);
                        TerBlue.DefaultBackColor = "FF000080".FromHEXToColor(true);
                        TerBlackB.DefaultBackColor = "FF808080".FromHEXToColor(true);
                        TerBlueB.DefaultBackColor = "FF0000FF".FromHEXToColor(true);
                        TerCyanB.DefaultBackColor = "FF00FFFF".FromHEXToColor(true);
                        TerGreenB.DefaultBackColor = "FF00FF00".FromHEXToColor(true);
                        TerPurpleB.DefaultBackColor = "FFFF00FF".FromHEXToColor(true);
                        TerRedB.DefaultBackColor = "FFFF0000".FromHEXToColor(true);
                        TerWhiteB.DefaultBackColor = "FFFFFFFF".FromHEXToColor(true);
                        TerYellowB.DefaultBackColor = "FFFFFF00".FromHEXToColor(true);
                        TerCursor.DefaultBackColor = "FFFFFFFF".FromHEXToColor(true);
                        TerCyan.DefaultBackColor = "FF008080".FromHEXToColor(true);
                        TerForeground.DefaultBackColor = "FFC0C0C0".FromHEXToColor(true);
                        TerGreen.DefaultBackColor = "FF008000".FromHEXToColor(true);
                        TerPurple.DefaultBackColor = "FF800080".FromHEXToColor(true);
                        TerRed.DefaultBackColor = "FF800000".FromHEXToColor(true);
                        TerSelection.DefaultBackColor = "FFFFFFFF".FromHEXToColor(true);
                        TerWhite.DefaultBackColor = "FFC0C0C0".FromHEXToColor(true);
                        TerYellow.DefaultBackColor = "FF808000".FromHEXToColor(true);
                        break;
                    }

                default:
                    {
                        TerBackground.DefaultBackColor = "FF0C0C0C".FromHEXToColor(true);
                        TerBlack.DefaultBackColor = "FF0C0C0C".FromHEXToColor(true);
                        TerBlue.DefaultBackColor = "FF0037DA".FromHEXToColor(true);
                        TerBlackB.DefaultBackColor = "FF767676".FromHEXToColor(true);
                        TerBlueB.DefaultBackColor = "FF3B78FF".FromHEXToColor(true);
                        TerCyanB.DefaultBackColor = "FF61D6D6".FromHEXToColor(true);
                        TerGreenB.DefaultBackColor = "FF16C60C".FromHEXToColor(true);
                        TerPurpleB.DefaultBackColor = "FFB4009E".FromHEXToColor(true);
                        TerRedB.DefaultBackColor = "FFE74856".FromHEXToColor(true);
                        TerWhiteB.DefaultBackColor = "FFF2F2F2".FromHEXToColor(true);
                        TerYellowB.DefaultBackColor = "FFF9F1A5".FromHEXToColor(true);
                        TerCursor.DefaultBackColor = "FFFFFFFF".FromHEXToColor(true);
                        TerCyan.DefaultBackColor = "FF3A96DD".FromHEXToColor(true);
                        TerForeground.DefaultBackColor = "FFCCCCCC".FromHEXToColor(true);
                        TerGreen.DefaultBackColor = "FF13A10E".FromHEXToColor(true);
                        TerPurple.DefaultBackColor = "FF881798".FromHEXToColor(true);
                        TerRed.DefaultBackColor = "FFC50F1F".FromHEXToColor(true);
                        TerSelection.DefaultBackColor = "FFFFFFFF".FromHEXToColor(true);
                        TerWhite.DefaultBackColor = "FFCCCCCC".FromHEXToColor(true);
                        TerYellow.DefaultBackColor = "FFC19C00".FromHEXToColor(true);
                        break;
                    }

            }
        }

        private void TerProfiles_SelectedIndexChanged(object sender, EventArgs e)
        {

            {
                TProfile temp = TerProfiles.SelectedIndex == 0 ? _Terminal.DefaultProf : _Terminal.Profiles[TerProfiles.SelectedIndex - 1];
                try
                {
                    if (TerSchemes.Items.Contains(temp.ColorScheme))
                        TerSchemes.SelectedItem = temp.ColorScheme;
                    else
                        TerSchemes.SelectedItem = _Terminal.DefaultProf.ColorScheme;
                }
                catch
                {
                }

                TerBackImage.Text = temp.BackgroundImage;
                TerImageOpacity.Value = (int)Math.Round(temp.BackgroundImageOpacity * 100f);

                TerCursorStyle.SelectedIndex = (int)temp.CursorShape;
                TerCursorHeightBar.Value = temp.CursorHeight;

                TerFontName.Text = temp.Font.Face;
                NativeMethods.GDI32.LogFont fx = new();
                Font f_cmd = new(temp.Font.Face, temp.Font.Size);
                f_cmd.ToLogFont(fx);
                fx.lfWeight = (int)temp.Font.Weight * 100;
                f_cmd = new(f_cmd.Name, f_cmd.Size, Font.FromLogFont(fx).Style);
                TerFontName.Font = new(f_cmd.Name, 9f, f_cmd.Style);

                TerFontSizeBar.Value = temp.Font.Size;
                TerFontWeight.SelectedIndex = (int)temp.Font.Weight;

                TerAcrylic.Checked = temp.UseAcrylic;
                TerOpacityBar.Value = temp.Opacity;

                Terminal1.Opacity = temp.Opacity;
                Terminal1.OpacityBackImage = temp.BackgroundImageOpacity * 100f;

                if (!string.IsNullOrEmpty(temp.TabTitle))
                {
                    Terminal1.TabTitle = temp.TabTitle;
                }
                else if (!string.IsNullOrEmpty(temp.Name))
                {
                    Terminal1.TabTitle = temp.Name;
                }
                else if (TerProfiles.SelectedIndex == 0)
                {
                    Terminal1.TabTitle = Program.Lang.Default;
                }
                else
                {
                    Terminal1.TabTitle = Program.Lang.Untitled;
                }

                if (File.Exists(temp.Icon))
                {
                    Terminal1.TabIcon = Bitmap_Mgr.Load(temp.Icon);
                }

                else
                {
                    IntPtr intPtr = IntPtr.Zero;
                    NativeMethods.Kernel32.Wow64DisableWow64FsRedirection(ref intPtr);
                    string path = string.Empty;
                    if (temp.Commandline is not null)
                        path = temp.Commandline.Replace("%SystemRoot%", PathsExt.Windows);
                    NativeMethods.Kernel32.Wow64RevertWow64FsRedirection(IntPtr.Zero);

                    if (File.Exists(path))
                    {
                        Terminal1.TabIcon = ((Icon)NativeMethods.DLLFunc.ExtractSmallIcon(path)).ToBitmap();
                    }
                    else
                    {
                        Terminal1.TabIcon = null;
                        Terminal1.TabIconButItIsString = "";
                    }

                }

            }

            ApplyPreview(_Terminal);

        }

        private void TerFontSizeBar_Scroll(object sender)
        {
            if (!_Shown)
                return;

            TerFontSizeVal.Text = ((UI.WP.Trackbar)sender).Value.ToString();

            Terminal1.Font = new(Terminal1.Font.Name, TerFontSizeBar.Value, Terminal1.Font.Style);

            {
                TProfile temp = TerProfiles.SelectedIndex == 0 ? _Terminal.DefaultProf : _Terminal.Profiles[TerProfiles.SelectedIndex - 1];
                temp.Font.Size = TerFontSizeBar.Value;
            }
        }

        private void TerCursorHeightBar_Scroll(object sender)
        {
            Terminal1.CursorHeight = Conversions.ToInteger(((UI.WP.Trackbar)sender).Value);
            Terminal1.Refresh();
            TerCursorHeightVal.Text = TerCursorHeightBar.Value.ToString();
            Terminal1.Refresh();

            if (!_Shown)
                return;

            {
                TProfile temp = TerProfiles.SelectedIndex == 0 ? _Terminal.DefaultProf : _Terminal.Profiles[TerProfiles.SelectedIndex - 1];
                temp.CursorHeight = Conversions.ToInteger(((UI.WP.Trackbar)sender).Value);
            }
        }

        private void TerImageOpacity_Scroll(object sender)
        {
            TerImageOpacityVal.Text = ((UI.WP.Trackbar)sender).Value.ToString();

            Terminal1.OpacityBackImage = TerImageOpacity.Value;

            if (!_Shown)
                return;


            {
                TProfile temp = TerProfiles.SelectedIndex == 0 ? _Terminal.DefaultProf : _Terminal.Profiles[TerProfiles.SelectedIndex - 1];
                temp.BackgroundImageOpacity = (float)(TerImageOpacity.Value / 100d);
            }
        }

        private void TerCursorStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            Terminal1.CursorType = (UI.Simulation.WinTerminal.CursorShape_Enum)TerCursorStyle.SelectedIndex;
            Terminal1.Refresh();

            if (!_Shown)
                return;

            {
                TProfile temp = TerProfiles.SelectedIndex == 0 ? _Terminal.DefaultProf : _Terminal.Profiles[TerProfiles.SelectedIndex - 1];
                temp.CursorShape = (TProfile.CursorShape_Enum)TerCursorStyle.SelectedIndex;
            }
        }

        private void Button12_Click(object sender, EventArgs e)
        {
            _Terminal.Colors.Add(new TColors() { Name = $"{Program.Lang.Terminal_NewScheme} #{TerSchemes.Items.Count}" });
            FillTerminalSchemes(_Terminal, TerSchemes);
            TerSchemes.SelectedIndex = TerSchemes.Items.Count - 1;
        }

        private void ColorClick(object sender, EventArgs e)
        {


            if (e is DragEventArgs)
                return;

            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                Forms.SubMenu.ShowMenu((UI.Controllers.ColorItem)sender);
                return;
            }

            List<Control> CList = new() { (Control)sender, Terminal1 };

            Conditions _conditions = new();
            if (((UI.Controllers.ColorItem)sender).Name.ToString().ToLower().Contains("ColorTable00".ToLower()))
                _conditions.CMD_ColorTable00 = true;
            if (((UI.Controllers.ColorItem)sender).Name.ToString().ToLower().Contains("ColorTable01".ToLower()))
                _conditions.CMD_ColorTable01 = true;
            if (((UI.Controllers.ColorItem)sender).Name.ToString().ToLower().Contains("ColorTable02".ToLower()))
                _conditions.CMD_ColorTable02 = true;
            if (((UI.Controllers.ColorItem)sender).Name.ToString().ToLower().Contains("ColorTable03".ToLower()))
                _conditions.CMD_ColorTable03 = true;
            if (((UI.Controllers.ColorItem)sender).Name.ToString().ToLower().Contains("ColorTable04".ToLower()))
                _conditions.CMD_ColorTable04 = true;
            if (((UI.Controllers.ColorItem)sender).Name.ToString().ToLower().Contains("ColorTable05".ToLower()))
                _conditions.CMD_ColorTable05 = true;
            if (((UI.Controllers.ColorItem)sender).Name.ToString().ToLower().Contains("ColorTable06".ToLower()))
                _conditions.CMD_ColorTable06 = true;
            if (((UI.Controllers.ColorItem)sender).Name.ToString().ToLower().Contains("ColorTable07".ToLower()))
                _conditions.CMD_ColorTable07 = true;
            if (((UI.Controllers.ColorItem)sender).Name.ToString().ToLower().Contains("ColorTable08".ToLower()))
                _conditions.CMD_ColorTable08 = true;
            if (((UI.Controllers.ColorItem)sender).Name.ToString().ToLower().Contains("ColorTable09".ToLower()))
                _conditions.CMD_ColorTable09 = true;
            if (((UI.Controllers.ColorItem)sender).Name.ToString().ToLower().Contains("ColorTable10".ToLower()))
                _conditions.CMD_ColorTable10 = true;
            if (((UI.Controllers.ColorItem)sender).Name.ToString().ToLower().Contains("ColorTable11".ToLower()))
                _conditions.CMD_ColorTable11 = true;
            if (((UI.Controllers.ColorItem)sender).Name.ToString().ToLower().Contains("ColorTable12".ToLower()))
                _conditions.CMD_ColorTable12 = true;
            if (((UI.Controllers.ColorItem)sender).Name.ToString().ToLower().Contains("ColorTable13".ToLower()))
                _conditions.CMD_ColorTable13 = true;
            if (((UI.Controllers.ColorItem)sender).Name.ToString().ToLower().Contains("ColorTable14".ToLower()))
                _conditions.CMD_ColorTable14 = true;
            if (((UI.Controllers.ColorItem)sender).Name.ToString().ToLower().Contains("ColorTable15".ToLower()))
                _conditions.CMD_ColorTable15 = true;


            Color C = Forms.ColorPickerDlg.Pick(CList, _conditions);

            ((UI.Controllers.ColorItem)sender).BackColor = C;
            ((UI.Controllers.ColorItem)sender).Invalidate();

            CList.Clear();
        }

        private void ColorMainsClick(object sender, EventArgs e)
        {

            if (e is DragEventArgs)
            {
                if (((UI.Controllers.ColorItem)sender).Name.ToString().ToLower().Contains(TerBackground.Name.ToLower()))
                {
                    _Terminal.Colors[TerSchemes.SelectedIndex].Background = ((UI.Controllers.ColorItem)sender).BackColor;
                }

                if (((UI.Controllers.ColorItem)sender).Name.ToString().ToLower().Contains(TerForeground.Name.ToLower()))
                {
                    _Terminal.Colors[TerSchemes.SelectedIndex].Foreground = ((UI.Controllers.ColorItem)sender).BackColor;
                }

                if (((UI.Controllers.ColorItem)sender).Name.ToString().ToLower().Contains(TerSelection.Name.ToLower()))
                {
                    _Terminal.Colors[TerSchemes.SelectedIndex].SelectionBackground = ((UI.Controllers.ColorItem)sender).BackColor;
                }

                if (((UI.Controllers.ColorItem)sender).Name.ToString().ToLower().Contains(TerCursor.Name.ToLower()))
                {
                    _Terminal.Colors[TerSchemes.SelectedIndex].CursorColor = ((UI.Controllers.ColorItem)sender).BackColor;
                }

                if (((UI.Controllers.ColorItem)sender).Name.ToString().ToLower().Contains(TerTabActive.Name.ToLower()))
                {
                    _Terminal.Themes[TerThemes.SelectedIndex - 3].Tab_Active = ((UI.Controllers.ColorItem)sender).BackColor;
                }

                if (((UI.Controllers.ColorItem)sender).Name.ToString().ToLower().Contains(TerTabInactive.Name.ToLower()))
                {
                    _Terminal.Themes[TerThemes.SelectedIndex - 3].Tab_Inactive = ((UI.Controllers.ColorItem)sender).BackColor;
                }

                if (((UI.Controllers.ColorItem)sender).Name.ToString().ToLower().Contains(TerTitlebarActive.Name.ToLower()))
                {
                    _Terminal.Themes[TerThemes.SelectedIndex - 3].Titlebar_Active = ((UI.Controllers.ColorItem)sender).BackColor;
                }

                if (((UI.Controllers.ColorItem)sender).Name.ToString().ToLower().Contains(TerTitlebarInactive.Name.ToLower()))
                {
                    _Terminal.Themes[TerThemes.SelectedIndex - 3].Titlebar_Inactive = ((UI.Controllers.ColorItem)sender).BackColor;
                }

                ApplyPreview(_Terminal);
                return;
            }

            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                Color cx = Forms.SubMenu.ShowMenu((UI.Controllers.ColorItem)sender, sender != TerBackground & sender != TerForeground & sender != TerSelection & sender != TerCursor);

                if (ColorClipboard.Event != ColorClipboard.MenuEvent.None)
                {
                    if (((UI.Controllers.ColorItem)sender).Name.ToString().ToLower().Contains(TerBackground.Name.ToLower()))
                    {
                        _Terminal.Colors[TerSchemes.SelectedIndex].Background = cx;
                    }

                    if (((UI.Controllers.ColorItem)sender).Name.ToString().ToLower().Contains(TerForeground.Name.ToLower()))
                    {
                        _Terminal.Colors[TerSchemes.SelectedIndex].Foreground = cx;
                    }

                    if (((UI.Controllers.ColorItem)sender).Name.ToString().ToLower().Contains(TerSelection.Name.ToLower()))
                    {
                        _Terminal.Colors[TerSchemes.SelectedIndex].SelectionBackground = cx;
                    }

                    if (((UI.Controllers.ColorItem)sender).Name.ToString().ToLower().Contains(TerCursor.Name.ToLower()))
                    {
                        _Terminal.Colors[TerSchemes.SelectedIndex].CursorColor = cx;
                    }

                    if (((UI.Controllers.ColorItem)sender).Name.ToString().ToLower().Contains(TerTabActive.Name.ToLower()))
                    {
                        _Terminal.Themes[TerThemes.SelectedIndex - 3].Tab_Active = cx;
                    }

                    if (((UI.Controllers.ColorItem)sender).Name.ToString().ToLower().Contains(TerTabInactive.Name.ToLower()))
                    {
                        _Terminal.Themes[TerThemes.SelectedIndex - 3].Tab_Inactive = cx;
                    }

                    if (((UI.Controllers.ColorItem)sender).Name.ToString().ToLower().Contains(TerTitlebarActive.Name.ToLower()))
                    {
                        _Terminal.Themes[TerThemes.SelectedIndex - 3].Titlebar_Active = cx;
                    }

                    if (((UI.Controllers.ColorItem)sender).Name.ToString().ToLower().Contains(TerTitlebarInactive.Name.ToLower()))
                    {
                        _Terminal.Themes[TerThemes.SelectedIndex - 3].Titlebar_Inactive = cx;
                    }

                    ApplyPreview(_Terminal);
                }

                return;
            }

            List<Control> CList = new() { (Control)sender, Terminal1, Terminal2 };

            Conditions _conditions = new();

            if (((UI.Controllers.ColorItem)sender).Name.ToString().ToLower().Contains(TerBackground.Name.ToLower()))
                _conditions.Terminal_Back = true;
            if (((UI.Controllers.ColorItem)sender).Name.ToString().ToLower().Contains(TerForeground.Name.ToLower()))
                _conditions.Terminal_Fore = true;
            if (((UI.Controllers.ColorItem)sender).Name.ToString().ToLower().Contains(TerSelection.Name.ToLower()))
                _conditions.Terminal_Selection = true;
            if (((UI.Controllers.ColorItem)sender).Name.ToString().ToLower().Contains(TerCursor.Name.ToLower()))
                _conditions.Terminal_Cursor = true;

            if (((UI.Controllers.ColorItem)sender).Name.ToString().ToLower().Contains(TerTabActive.Name.ToLower()))
                _conditions.Terminal_TabActive = true;
            if (((UI.Controllers.ColorItem)sender).Name.ToString().ToLower().Contains(TerTabInactive.Name.ToLower()))
                _conditions.Terminal_TabInactive = true;
            if (((UI.Controllers.ColorItem)sender).Name.ToString().ToLower().Contains(TerTitlebarActive.Name.ToLower()))
                _conditions.Terminal_TitlebarActive = true;
            if (((UI.Controllers.ColorItem)sender).Name.ToString().ToLower().Contains(TerTitlebarInactive.Name.ToLower()))
                _conditions.Terminal_TitlebarInactive = true;


            Color C = Forms.ColorPickerDlg.Pick(CList, _conditions);

            if (((UI.Controllers.ColorItem)sender).Name.ToString().ToLower().Contains(TerBackground.Name.ToLower()))
            {
                _Terminal.Colors[TerSchemes.SelectedIndex].Background = C;
            }

            if (((UI.Controllers.ColorItem)sender).Name.ToString().ToLower().Contains(TerForeground.Name.ToLower()))
            {
                _Terminal.Colors[TerSchemes.SelectedIndex].Foreground = C;
            }

            if (((UI.Controllers.ColorItem)sender).Name.ToString().ToLower().Contains(TerSelection.Name.ToLower()))
            {
                _Terminal.Colors[TerSchemes.SelectedIndex].SelectionBackground = C;
            }

            if (((UI.Controllers.ColorItem)sender).Name.ToString().ToLower().Contains(TerCursor.Name.ToLower()))
            {
                _Terminal.Colors[TerSchemes.SelectedIndex].CursorColor = C;
            }

            if (((UI.Controllers.ColorItem)sender).Name.ToString().ToLower().Contains(TerTabActive.Name.ToLower()))
            {
                _Terminal.Themes[TerThemes.SelectedIndex - 3].Tab_Active = C;
            }

            if (((UI.Controllers.ColorItem)sender).Name.ToString().ToLower().Contains(TerTabInactive.Name.ToLower()))
            {
                _Terminal.Themes[TerThemes.SelectedIndex - 3].Tab_Inactive = C;
            }

            if (((UI.Controllers.ColorItem)sender).Name.ToString().ToLower().Contains(TerTitlebarActive.Name.ToLower()))
            {
                _Terminal.Themes[TerThemes.SelectedIndex - 3].Titlebar_Active = C;
            }

            if (((UI.Controllers.ColorItem)sender).Name.ToString().ToLower().Contains(TerTitlebarInactive.Name.ToLower()))
            {
                _Terminal.Themes[TerThemes.SelectedIndex - 3].Titlebar_Inactive = C;
            }

            ApplyPreview(_Terminal);

            ((UI.Controllers.ColorItem)sender).BackColor = C;
            ((UI.Controllers.ColorItem)sender).Invalidate();

            CList.Clear();
        }

        public void ApplyPreview(WinTerminal Terminal)
        {
            try
            {
                Terminal1.UseAcrylicOnTitlebar = Terminal.UseAcrylicInTabRow;

                if (TerProfiles.SelectedIndex == 0)
                {
                    Terminal1.TabColor = Terminal.DefaultProf.TabColor;
                }
                else if (Terminal.Profiles[TerProfiles.SelectedIndex - 1].TabColor == Color.FromArgb(0, 0, 0, 0))
                {
                    Terminal1.TabColor = Terminal.DefaultProf.TabColor;
                }
                else
                {
                    Terminal1.TabColor = Terminal.Profiles[TerProfiles.SelectedIndex - 1].TabColor;
                }

                Terminal1.Color_Background = Terminal.Colors[TerSchemes.SelectedIndex].Background;
                Terminal1.Color_Foreground = Terminal.Colors[TerSchemes.SelectedIndex].Foreground;
                Terminal1.Color_Selection = Terminal.Colors[TerSchemes.SelectedIndex].SelectionBackground;
                Terminal1.Color_Cursor = Terminal.Colors[TerSchemes.SelectedIndex].CursorColor;

                Terminal2.Color_Background = Terminal.Colors[TerSchemes.SelectedIndex].Background;
                Terminal2.Color_Foreground = Terminal.Colors[TerSchemes.SelectedIndex].Foreground;
                Terminal2.Color_Selection = Terminal.Colors[TerSchemes.SelectedIndex].SelectionBackground;
                Terminal2.Color_Cursor = Terminal.Colors[TerSchemes.SelectedIndex].CursorColor;

                if (TerThemesContainer.Enabled)
                {
                    Terminal1.Color_TabFocused = Terminal.Themes[TerThemes.SelectedIndex - 3].Tab_Active;
                    Terminal1.Color_Titlebar = Terminal.Themes[TerThemes.SelectedIndex - 3].Titlebar_Active;
                    Terminal1.Color_TabUnFocused = Terminal.Themes[TerThemes.SelectedIndex - 3].Tab_Inactive;
                    Terminal2.Color_Titlebar_Unfocused = Terminal.Themes[TerThemes.SelectedIndex - 3].Titlebar_Inactive;
                }
                else
                {
                    Terminal1.Color_TabFocused = Color.FromArgb(0, 0, 0, 0);
                    Terminal1.Color_Titlebar = Color.FromArgb(0, 0, 0, 0);
                    Terminal1.Color_Titlebar_Unfocused = Color.FromArgb(0, 0, 0, 0);
                    Terminal1.Color_TabUnFocused = Color.FromArgb(0, 0, 0, 0);
                    Terminal2.Color_Titlebar_Unfocused = Color.FromArgb(0, 0, 0, 0);
                }

                if (TerThemes.SelectedItem is not null)
                {
                    if (TerThemes.SelectedItem.ToString().ToLower() == "dark")
                    {
                        Terminal1.Light = false;
                        Terminal2.Light = false;
                    }

                    else if (TerThemes.SelectedItem.ToString().ToLower() == "light")
                    {
                        Terminal1.Light = true;
                        Terminal2.Light = true;
                    }

                    else if (TerThemes.SelectedItem.ToString().ToLower() == "system")
                    {
                        switch (Program.PreviewStyle)
                        {
                            case WindowStyle.W12:
                                {
                                    Terminal1.Light = Program.TM.Windows12.AppMode_Light;
                                    Terminal2.Light = Program.TM.Windows12.AppMode_Light;
                                    break;
                                }

                            case WindowStyle.W11:
                                {
                                    Terminal1.Light = Program.TM.Windows11.AppMode_Light;
                                    Terminal2.Light = Program.TM.Windows11.AppMode_Light;
                                    break;
                                }

                            case WindowStyle.W10:
                                {
                                    Terminal1.Light = Program.TM.Windows10.AppMode_Light;
                                    Terminal2.Light = Program.TM.Windows10.AppMode_Light;
                                    break;
                                }

                            default:
                                {
                                    Terminal1.Light = Program.TM.Windows12.AppMode_Light;
                                    Terminal2.Light = Program.TM.Windows12.AppMode_Light;
                                    break;
                                }
                        }
                    }

                    else
                    {
                        Terminal1.Light = !TerMode.Checked;
                        Terminal2.Light = !TerMode.Checked;
                    }
                }

                {
                    TProfile temp = TerProfiles.SelectedIndex == 0 ? _Terminal.DefaultProf : _Terminal.Profiles[TerProfiles.SelectedIndex - 1];
                    NativeMethods.GDI32.LogFont fx = new();
                    Font f_cmd = new(temp.Font.Face, temp.Font.Size);
                    f_cmd.ToLogFont(fx);
                    fx.lfWeight = (int)temp.Font.Weight * 100;
                    f_cmd = new(f_cmd.Name, f_cmd.Size, Font.FromLogFont(fx).Style);
                    Terminal1.Font = f_cmd;
                    TerFontSizeVal.Text = f_cmd.Size.ToString();
                }

                Terminal1.Refresh();
                Terminal2.Refresh();
            }
            catch
            {

            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            _Terminal.Themes.Add(new TTheme() { Name = $"{Program.Lang.Terminal_NewTheme} #{(TerThemes.Items.Count - 3)}" });
            FillTerminalThemes(_Terminal, TerThemes);
            TerThemes.SelectedIndex = TerThemes.Items.Count - 1;
        }

        private void TerFontWeight_SelectedIndexChanged(object sender, EventArgs e)
        {
            NativeMethods.GDI32.LogFont fx = new();
            Font f_cmd = new(Terminal1.Font.Name, Terminal1.Font.Size, Terminal1.Font.Style);
            f_cmd.ToLogFont(fx);
            fx.lfWeight = TerFontWeight.SelectedIndex * 100;
            {
                Font temp = Font.FromLogFont(fx);
                f_cmd = new(Terminal1.Font.Name, Terminal1.Font.Size, temp.Style);
            }
            Terminal1.Font = f_cmd;
            Terminal1.Refresh();

            {
                TProfile temp1 = TerProfiles.SelectedIndex == 0 ? _Terminal.DefaultProf : _Terminal.Profiles[TerProfiles.SelectedIndex - 1];
                temp1.Font.Weight = (TProfile.FontWeight_Enum)TerFontWeight.SelectedIndex;
            }

        }

        private void TerThemes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_Shown)
                return;

            if (TerThemes.SelectedIndex > 2)
            {
                TerThemesContainer.Enabled = true;

                {
                    TTheme temp = _Terminal.Themes[TerThemes.SelectedIndex - 3];
                    TerTitlebarActive.BackColor = temp.Titlebar_Active;
                    TerTitlebarInactive.BackColor = temp.Titlebar_Inactive;
                    TerTabActive.BackColor = temp.Tab_Active;
                    TerTabInactive.BackColor = temp.Tab_Inactive;
                    TerMode.Checked = !(temp.Style.ToLower() == "light");
                }
            }

            else
            {
                TerThemesContainer.Enabled = false;

                TerTitlebarActive.BackColor = Color.FromArgb(0, 0, 0, 0);
                TerTitlebarInactive.BackColor = Color.FromArgb(0, 0, 0, 0);
                TerTabActive.BackColor = Color.FromArgb(0, 0, 0, 0);
                TerTabInactive.BackColor = Color.FromArgb(0, 0, 0, 0);

                if (TerThemes.SelectedIndex == 0)
                    TerMode.Checked = true;
                if (TerThemes.SelectedIndex == 1)
                    TerMode.Checked = false;

                switch (Program.PreviewStyle)
                {
                    case WindowStyle.W12:
                        {
                            if (TerThemes.SelectedIndex == 2)
                                TerMode.Checked = !Program.TM.Windows12.AppMode_Light;
                            break;
                        }

                    case WindowStyle.W11:
                        {
                            if (TerThemes.SelectedIndex == 2)
                                TerMode.Checked = !Program.TM.Windows11.AppMode_Light;
                            break;
                        }

                    case WindowStyle.W10:
                        {
                            if (TerThemes.SelectedIndex == 2)
                                TerMode.Checked = !Program.TM.Windows10.AppMode_Light;
                            break;
                        }

                    default:
                        {
                            if (TerThemes.SelectedIndex == 2)
                                TerMode.Checked = !Program.TM.Windows12.AppMode_Light;
                            break;
                        }

                }


            }

            if (TerThemes.SelectedItem.ToString().ToLower() == "dark")
            {
                _Terminal.Theme = "dark";
            }

            else if (TerThemes.SelectedItem.ToString().ToLower() == "light")
            {
                _Terminal.Theme = "light";
            }

            else if (TerThemes.SelectedItem.ToString().ToLower() == "system")
            {
                _Terminal.Theme = "system";
            }

            else
            {
                _Terminal.Theme = TerThemes.SelectedItem.ToString();

            }

            ApplyPreview(_Terminal);
        }

        private void TerEditThemeName_Click(object sender, EventArgs e)
        {
            if (TerThemes.SelectedIndex > 2)
            {
                string s = InputBox(Program.Lang.Terminal_TypeSchemeName, TerThemes.SelectedItem.ToString());
                if ((s ?? string.Empty) != (TerThemes.SelectedItem.ToString() ?? string.Empty) & !string.IsNullOrEmpty(s) & !TerThemes.Items.Contains(s))
                {
                    int i = TerThemes.SelectedIndex;
                    TerThemes.Items.RemoveAt(i);
                    TerThemes.Items.Insert(i, s);
                    TerThemes.SelectedIndex = i;
                    _Terminal.Themes[i - 3].Name = s;
                }
            }
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            string s = InputBox(Program.Lang.Terminal_TypeSchemeName, TerSchemes.SelectedItem.ToString());
            if ((s ?? string.Empty) != (TerSchemes.SelectedItem.ToString() ?? string.Empty) & !string.IsNullOrEmpty(s) & !TerSchemes.Items.Contains(s))
            {
                int i = TerSchemes.SelectedIndex;
                TerSchemes.Items.RemoveAt(i);
                TerSchemes.Items.Insert(i, s);
                TerSchemes.SelectedIndex = i;
                _Terminal.Colors[i].Name = s;
            }
        }

        private void Button14_Click(object sender, EventArgs e)
        {
            Forms.TerminalInfo.Profile = TerProfiles.SelectedIndex == 0 ? _Terminal.DefaultProf : _Terminal.Profiles[TerProfiles.SelectedIndex - 1];
            if (Forms.TerminalInfo.OpenDialog(TerProfiles.SelectedIndex == 0) == DialogResult.OK)
            {

                {
                    TProfile temp = TerProfiles.SelectedIndex == 0 ? _Terminal.DefaultProf : _Terminal.Profiles[TerProfiles.SelectedIndex - 1];
                    temp.Name = Forms.TerminalInfo.Profile.Name;
                    temp.TabTitle = Forms.TerminalInfo.Profile.TabTitle;
                    temp.Icon = Forms.TerminalInfo.Profile.Icon;
                    temp.TabColor = Forms.TerminalInfo.Profile.TabColor;
                }

                int i = TerProfiles.SelectedIndex;
                FillTerminalProfiles(_Terminal, TerProfiles);
                TerProfiles.SelectedIndex = i;

                ApplyPreview(_Terminal);
            }
        }

        private void TerAcrylicBar_Scroll(object sender)
        {
            TerOpacityVal.Text = TerOpacityBar.Value.ToString();

            Terminal1.Opacity = TerOpacityBar.Value;

            Terminal1.Refresh();

            if (_Shown)
            {
                {
                    TProfile temp = TerProfiles.SelectedIndex == 0 ? _Terminal.DefaultProf : _Terminal.Profiles[TerProfiles.SelectedIndex - 1];
                    temp.Opacity = TerOpacityBar.Value;
                }
            }

        }

        private void TerAcrylic_CheckedChanged(object sender)
        {
            Terminal1.UseAcrylic = TerAcrylic.Checked;

            Terminal1.Refresh();

            if (!_Shown)
                return;

            {
                TProfile temp = TerProfiles.SelectedIndex == 0 ? _Terminal.DefaultProf : _Terminal.Profiles[TerProfiles.SelectedIndex - 1];
                temp.UseAcrylic = TerAcrylic.Checked;
            }
        }

        private void CheckBox1_CheckedChanged(object sender)
        {
            Program.Settings.WindowsTerminals.ListAllFonts = CheckBox1.Checked;
            Program.Settings.WindowsTerminals.Save();
        }

        private void Button13_Click(object sender, EventArgs e)
        {
            _Terminal.Profiles.Add(new TProfile() { Name = $"{Program.Lang.Terminal_NewProfile} #{TerProfiles.Items.Count}", ColorScheme = _Terminal.DefaultProf.ColorScheme });
            FillTerminalProfiles(_Terminal, TerProfiles);
            TerProfiles.SelectedIndex = TerProfiles.Items.Count - 1;
        }

        private void Button15_Click(object sender, EventArgs e)
        {
            string TerDir;
            string TerPreDir;

            if (!Program.Settings.WindowsTerminals.Path_Deflection)
            {
                TerDir = PathsExt.TerminalJSON;
                TerPreDir = PathsExt.TerminalPreviewJSON;
            }
            else
            {
                if (File.Exists(Program.Settings.WindowsTerminals.Terminal_Stable_Path))
                {
                    TerDir = Program.Settings.WindowsTerminals.Terminal_Stable_Path;
                }
                else
                {
                    TerDir = PathsExt.TerminalJSON;
                }

                if (File.Exists(Program.Settings.WindowsTerminals.Terminal_Preview_Path))
                {
                    TerPreDir = Program.Settings.WindowsTerminals.Terminal_Preview_Path;
                }
                else
                {
                    TerPreDir = PathsExt.TerminalPreviewJSON;
                }
            }

            switch (_Mode)
            {
                case WinTerminal.Version.Stable:
                    {
                        if (File.Exists(TerDir))
                            Interaction.Shell(@"explorer.exe shell:appsFolder\Microsoft.WindowsTerminal_8wekyb3d8bbwe!App");
                        break;
                    }

                case WinTerminal.Version.Preview:
                    {
                        if (File.Exists(TerPreDir))
                            Interaction.Shell(@"explorer.exe shell:appsFolder\Microsoft.WindowsTerminalPreview_8wekyb3d8bbwe!App");
                        break;
                    }

            }


        }

        private void Button5_Click(object sender, EventArgs e)
        {
            TerBackImage.Text = "desktopWallpaper";
        }

        private void TerBackImage_TextChanged(object sender, EventArgs e)
        {
            if (TerBackImage.Text == "desktopWallpaper")
            {
                Terminal1.BackImage = Program.Wallpaper;
            }
            else if (File.Exists(TerBackImage.Text))
            {
                Terminal1.BackImage = Bitmap_Mgr.Load(TerBackImage.Text).FillScale(Terminal1.Size).Resize(Terminal1.Width - 2, Terminal1.Height - 32);
            }

            else
            {
                Terminal1.BackImage = null;
            }

            if (!_Shown)
                return;

            {
                TProfile temp = TerProfiles.SelectedIndex == 0 ? _Terminal.DefaultProf : _Terminal.Profiles[TerProfiles.SelectedIndex - 1];
                temp.BackgroundImage = TerBackImage.Text;
            }

            Terminal1.Invalidate();
        }

        private void Button16_Click(object sender, EventArgs e)
        {
            if (ImgDlg.ShowDialog() == DialogResult.OK)
            {
                TerBackImage.Text = ImgDlg.FileName;
            }
        }

        private void TerMode_CheckedChanged(object sender, EventArgs e)
        {
            if (TerThemes.SelectedIndex > 2)
            {
                _Terminal.Themes[TerThemes.SelectedIndex - 3].Style = !TerMode.Checked ? "light" : "dark";
            }

            if (_Shown)
                ApplyPreview(_Terminal);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            switch (_Mode)
            {
                case WinTerminal.Version.Stable:
                    {
                        Program.TM.Terminal.Enabled = TerEnabled.Checked;
                        Program.TM.Terminal = _Terminal;
                        break;
                    }

                case WinTerminal.Version.Preview:
                    {
                        Program.TM.TerminalPreview.Enabled = TerEnabled.Checked;
                        Program.TM.TerminalPreview = _Terminal;
                        break;
                    }

            }

            DialogResult = DialogResult.OK;

            Close();
        }

        private void WindowsTerminal_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult != DialogResult.OK)
            {


                switch (_Mode)
                {
                    case WinTerminal.Version.Stable:
                        {
                            Program.TM.Terminal = _TerminalDefault;
                            break;
                        }

                    case WinTerminal.Version.Preview:
                        {
                            Program.TM.TerminalPreview = _TerminalDefault;
                            break;
                        }

                }

            }

            DialogResult = DialogResult.Cancel;
        }

        private void Button10_Click(object sender, EventArgs e)
        {

            if (TerEnabled.Checked)
            {
                if (OS.W12 || OS.W11 || OS.W10)
                {

                    try
                    {
                        Cursor = Cursors.WaitCursor;

                        string TerDir;
                        string TerPreDir;

                        if (!Program.Settings.WindowsTerminals.Path_Deflection)
                        {
                            TerDir = PathsExt.TerminalJSON;
                            TerPreDir = PathsExt.TerminalPreviewJSON;
                        }
                        else
                        {
                            if (File.Exists(Program.Settings.WindowsTerminals.Terminal_Stable_Path))
                            {
                                TerDir = Program.Settings.WindowsTerminals.Terminal_Stable_Path;
                            }
                            else
                            {
                                TerDir = PathsExt.TerminalJSON;
                            }

                            if (File.Exists(Program.Settings.WindowsTerminals.Terminal_Preview_Path))
                            {
                                TerPreDir = Program.Settings.WindowsTerminals.Terminal_Preview_Path;
                            }
                            else
                            {
                                TerPreDir = PathsExt.TerminalPreviewJSON;
                            }
                        }

                        if (File.Exists(TerDir) & _Mode == WinTerminal.Version.Stable)
                        {
                            _Terminal.Save(TerDir, WinTerminal.Mode.JSONFile);
                        }

                        if (File.Exists(TerPreDir) & _Mode == WinTerminal.Version.Preview)
                        {
                            _Terminal.Save(TerPreDir, WinTerminal.Mode.JSONFile, WinTerminal.Version.Preview);
                        }

                        Cursor = Cursors.Default;
                    }

                    catch (Exception ex)
                    {
                        throw ex;
                    }

                }
            }

            else
            {
                MsgBox(Program.Lang.CMD_Enable, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Button11_Click(object sender, EventArgs e)
        {
            if (OS.W12 || OS.W11 || OS.W10)
            {
                string TerDir;
                string TerPreDir;

                TerDir = PathsExt.TerminalJSON;
                TerPreDir = PathsExt.TerminalPreviewJSON;


                if (File.Exists(TerDir) & _Mode == WinTerminal.Version.Stable)
                {
                    Process.Start(TerDir);
                }

                if (File.Exists(TerPreDir) & _Mode == WinTerminal.Version.Preview)
                {
                    Process.Start(TerPreDir);
                }

            }
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            if (SaveJSONDlg.ShowDialog() == DialogResult.OK)
            {
                if (OS.W12 || OS.W11 || OS.W10)
                {
                    string TerDir;
                    string TerPreDir;

                    TerDir = PathsExt.TerminalJSON;
                    TerPreDir = PathsExt.TerminalPreviewJSON;

                    if (File.Exists(TerDir) & _Mode == WinTerminal.Version.Stable)
                    {
                        File.Copy(TerDir, SaveJSONDlg.FileName);
                    }

                    if (File.Exists(TerPreDir) & _Mode == WinTerminal.Version.Preview)
                    {
                        File.Copy(TerPreDir, SaveJSONDlg.FileName);
                    }

                }
            }
        }

        public WinTerminal.Version SaveState;

        private void Button8_Click(object sender, EventArgs e)
        {
            if (OpenWPTHDlg.ShowDialog() == DialogResult.OK)
            {

                SaveState = _Mode;
                if (Forms.WindowsTerminalDecide.ShowDialog() == DialogResult.OK)
                {
                    if (SaveState == WinTerminal.Version.Stable)
                    {
                        _Terminal = new(OpenWPTHDlg.FileName, WinTerminal.Mode.WinPaletterFile);
                    }

                    else if (SaveState == WinTerminal.Version.Preview)
                    {
                        _Terminal = new(OpenWPTHDlg.FileName, WinTerminal.Mode.WinPaletterFile, WinTerminal.Version.Preview);

                    }

                    Load_FromTerminal();
                }

            }
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            if (OpenJSONDlg.ShowDialog() == DialogResult.OK)
            {

                try
                {

                    if (_Mode == WinTerminal.Version.Stable)
                    {
                        _Terminal = new(OpenJSONDlg.FileName, WinTerminal.Mode.JSONFile);
                        Load_FromTerminal();
                    }

                    if (_Mode == WinTerminal.Version.Preview)
                    {
                        _Terminal = new(OpenJSONDlg.FileName, WinTerminal.Mode.JSONFile, WinTerminal.Version.Preview);
                        Load_FromTerminal();
                    }
                }

                catch (Exception ex)
                {
                    MsgBox(Program.Lang.Terminal_ErrorFile, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Forms.BugReport.ThrowError(ex);
                }

            }
        }

        private void Button17_Click(object sender, EventArgs e)
        {

            TColors TC = new()
            {
                Name = $"{TerSchemes.SelectedItem} Clone #{TerSchemes.Items.Count}",
                Background = _Terminal.Colors[TerSchemes.SelectedIndex].Background,
                Black = _Terminal.Colors[TerSchemes.SelectedIndex].Black,
                Blue = _Terminal.Colors[TerSchemes.SelectedIndex].Blue,
                BrightBlack = _Terminal.Colors[TerSchemes.SelectedIndex].BrightBlack,
                BrightBlue = _Terminal.Colors[TerSchemes.SelectedIndex].BrightBlue,
                BrightCyan = _Terminal.Colors[TerSchemes.SelectedIndex].BrightCyan,
                BrightGreen = _Terminal.Colors[TerSchemes.SelectedIndex].BrightGreen,
                BrightPurple = _Terminal.Colors[TerSchemes.SelectedIndex].BrightPurple,
                BrightRed = _Terminal.Colors[TerSchemes.SelectedIndex].BrightRed,
                BrightWhite = _Terminal.Colors[TerSchemes.SelectedIndex].BrightWhite,
                BrightYellow = _Terminal.Colors[TerSchemes.SelectedIndex].BrightYellow,
                CursorColor = _Terminal.Colors[TerSchemes.SelectedIndex].CursorColor,
                Cyan = _Terminal.Colors[TerSchemes.SelectedIndex].Cyan,
                Foreground = _Terminal.Colors[TerSchemes.SelectedIndex].Foreground,
                Green = _Terminal.Colors[TerSchemes.SelectedIndex].Green,
                Purple = _Terminal.Colors[TerSchemes.SelectedIndex].Purple,
                Red = _Terminal.Colors[TerSchemes.SelectedIndex].Red,
                SelectionBackground = _Terminal.Colors[TerSchemes.SelectedIndex].SelectionBackground,
                White = _Terminal.Colors[TerSchemes.SelectedIndex].White,
                Yellow = _Terminal.Colors[TerSchemes.SelectedIndex].Yellow
            };

            _Terminal.Colors.Add(TC);
            FillTerminalSchemes(_Terminal, TerSchemes);
            TerSchemes.SelectedIndex = TerSchemes.Items.Count - 1;
        }

        private void Button18_Click(object sender, EventArgs e)
        {

            if (TerProfiles.SelectedIndex == 0)
            {
                MsgBox(Program.Lang.Terminal_ProfileNotCloneable, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            TProfile P = new()
            {
                Name = $"{_Terminal.Profiles[TerProfiles.SelectedIndex - 1].Name} {Program.Lang.Terminal_Clone} #{TerProfiles.Items.Count}",
                BackgroundImage = _Terminal.Profiles[TerProfiles.SelectedIndex - 1].BackgroundImage,
                BackgroundImageOpacity = _Terminal.Profiles[TerProfiles.SelectedIndex - 1].BackgroundImageOpacity,
                ColorScheme = _Terminal.Profiles[TerProfiles.SelectedIndex - 1].ColorScheme,
                Commandline = _Terminal.Profiles[TerProfiles.SelectedIndex - 1].Commandline,
                CursorHeight = _Terminal.Profiles[TerProfiles.SelectedIndex - 1].CursorHeight,
                CursorShape = _Terminal.Profiles[TerProfiles.SelectedIndex - 1].CursorShape,
                Font = _Terminal.Profiles[TerProfiles.SelectedIndex - 1].Font,
                Icon = _Terminal.Profiles[TerProfiles.SelectedIndex - 1].Icon,
                Opacity = _Terminal.Profiles[TerProfiles.SelectedIndex - 1].Opacity,
                TabColor = _Terminal.Profiles[TerProfiles.SelectedIndex - 1].TabColor,
                TabTitle = _Terminal.Profiles[TerProfiles.SelectedIndex - 1].TabTitle,
                UseAcrylic = _Terminal.Profiles[TerProfiles.SelectedIndex - 1].UseAcrylic
            };

            _Terminal.Profiles.Add(P);
            FillTerminalProfiles(_Terminal, TerProfiles);
            TerProfiles.SelectedIndex = TerProfiles.Items.Count - 1;

        }

        private void Button19_Click(object sender, EventArgs e)
        {
            if (TerThemes.SelectedIndex < 3)
            {
                MsgBox(Program.Lang.Terminal_ThemeNotCloneable, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            TTheme Th = new()
            {
                Name = $"{_Terminal.Themes[TerThemes.SelectedIndex - 3].Name} {Program.Lang.Terminal_Clone} #{TerThemes.Items.Count}",
                Style = _Terminal.Themes[TerThemes.SelectedIndex - 3].Style,
                Tab_Active = _Terminal.Themes[TerThemes.SelectedIndex - 3].Tab_Active,
                Tab_Inactive = _Terminal.Themes[TerThemes.SelectedIndex - 3].Tab_Inactive,
                Titlebar_Active = _Terminal.Themes[TerThemes.SelectedIndex - 3].Titlebar_Active,
                Titlebar_Inactive = _Terminal.Themes[TerThemes.SelectedIndex - 3].Titlebar_Inactive
            };

            _Terminal.Themes.Add(Th);
            FillTerminalThemes(_Terminal, TerThemes);
            TerThemes.SelectedIndex = TerThemes.Items.Count - 1;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;

            Close();
        }

        public string CCat;

        private void Button6_Click(object sender, EventArgs e)
        {
            {
                UI.WP.ComboBox temp = Forms.WindowsTerminalCopycat.ComboBox1;
                temp.Items.Clear();
                CCat = null;

                foreach (object x in TerProfiles.Items)
                    temp.Items.Add(x);
            }

            if (Forms.WindowsTerminalCopycat.ShowDialog() == DialogResult.OK)
            {
                for (int x = 0, loopTo = TerProfiles.Items.Count - 1; x <= loopTo; x++)
                {
                    if ((TerProfiles.Items[x].ToString().ToLower() ?? string.Empty) == (CCat.ToLower() ?? string.Empty))
                    {

                        TProfile CCatFrom = x == 0 ? _Terminal.DefaultProf : _Terminal.Profiles[x - 1];

                        {
                            TProfile temp1 = TerProfiles.SelectedIndex == 0 ? _Terminal.DefaultProf : _Terminal.Profiles[TerProfiles.SelectedIndex - 1];
                            temp1.BackgroundImage = CCatFrom.BackgroundImage;
                            temp1.BackgroundImageOpacity = CCatFrom.BackgroundImageOpacity;
                            temp1.ColorScheme = CCatFrom.ColorScheme;
                            temp1.CursorHeight = CCatFrom.CursorHeight;
                            temp1.CursorShape = CCatFrom.CursorShape;
                            temp1.Font.Face = CCatFrom.Font.Face;
                            temp1.Font.Weight = CCatFrom.Font.Weight;
                            temp1.Font.Size = CCatFrom.Font.Size;
                            temp1.Icon = CCatFrom.Icon;
                            temp1.Opacity = CCatFrom.Opacity;
                            temp1.TabColor = CCatFrom.TabColor;
                            temp1.TabTitle = CCatFrom.TabTitle;
                            temp1.UseAcrylic = CCatFrom.UseAcrylic;
                        }

                        try
                        {
                            if (TerSchemes.Items.Contains(CCatFrom.ColorScheme))
                                TerSchemes.SelectedItem = CCatFrom.ColorScheme;
                            else
                                TerSchemes.SelectedItem = _Terminal.DefaultProf.ColorScheme;
                        }
                        catch
                        {
                        }

                        TerBackImage.Text = CCatFrom.BackgroundImage;
                        TerImageOpacity.Value = (int)Math.Round(CCatFrom.BackgroundImageOpacity * 100f);

                        TerCursorStyle.SelectedIndex = (int)CCatFrom.CursorShape;
                        TerCursorHeightBar.Value = CCatFrom.CursorHeight;

                        TerFontName.Text = CCatFrom.Font.Face;
                        NativeMethods.GDI32.LogFont fx = new();
                        Font f_cmd = new(CCatFrom.Font.Face, CCatFrom.Font.Size);
                        f_cmd.ToLogFont(fx);
                        fx.lfWeight = (int)CCatFrom.Font.Weight * 100;
                        f_cmd = new(f_cmd.Name, f_cmd.Size, Font.FromLogFont(fx).Style);
                        TerFontName.Font = new(f_cmd.Name, 9f, f_cmd.Style);

                        TerFontSizeBar.Value = CCatFrom.Font.Size;
                        TerFontWeight.SelectedIndex = (int)CCatFrom.Font.Weight;

                        TerAcrylic.Checked = CCatFrom.UseAcrylic;
                        TerOpacityBar.Value = CCatFrom.Opacity;

                        Terminal1.Opacity = CCatFrom.Opacity;
                        Terminal1.OpacityBackImage = CCatFrom.BackgroundImageOpacity * 100f;

                        if (!string.IsNullOrEmpty(CCatFrom.TabTitle))
                        {
                            Terminal1.TabTitle = CCatFrom.TabTitle;
                        }
                        else if (!string.IsNullOrEmpty(CCatFrom.Name))
                        {
                            Terminal1.TabTitle = CCatFrom.Name;
                        }
                        else if (TerProfiles.SelectedIndex == 0)
                        {
                            Terminal1.TabTitle = Program.Lang.Default;
                        }
                        else
                        {
                            Terminal1.TabTitle = Program.Lang.Untitled;
                        }

                        if (File.Exists(CCatFrom.Icon))
                        {
                            Terminal1.TabIcon = Bitmap_Mgr.Load(CCatFrom.Icon);
                        }

                        else
                        {
                            IntPtr intPtr = IntPtr.Zero;
                            NativeMethods.Kernel32.Wow64DisableWow64FsRedirection(ref intPtr);
                            string path = string.Empty;
                            if (CCatFrom.Commandline is not null)
                                path = CCatFrom.Commandline.Replace("%SystemRoot%", PathsExt.Windows);
                            NativeMethods.Kernel32.Wow64RevertWow64FsRedirection(IntPtr.Zero);

                            if (File.Exists(path))
                            {
                                Terminal1.TabIcon = ((Icon)NativeMethods.DLLFunc.ExtractSmallIcon(path)).ToBitmap();
                            }
                            else
                            {
                                Terminal1.TabIcon = null;
                                Terminal1.TabIconButItIsString = "";
                            }


                        }

                        break;
                    }

                }

                ApplyPreview(_Terminal);

            }

        }

        private void Button20_Click(object sender, EventArgs e)
        {
            {
                UI.WP.ComboBox temp = Forms.WindowsTerminalCopycat.ComboBox1;
                temp.Items.Clear();
                CCat = null;

                foreach (object x in TerSchemes.Items)
                    temp.Items.Add(x);
            }

            if (Forms.WindowsTerminalCopycat.ShowDialog() == DialogResult.OK)
            {
                for (int x = 0, loopTo = TerSchemes.Items.Count - 1; x <= loopTo; x++)
                {
                    if ((TerSchemes.Items[x].ToString().ToLower() ?? string.Empty) == (CCat.ToLower() ?? string.Empty))
                    {

                        TColors CCatFrom = _Terminal.Colors[x];

                        {
                            TColors temp1 = _Terminal.Colors[TerSchemes.SelectedIndex];
                            temp1.Background = CCatFrom.Background;
                            temp1.Black = CCatFrom.Black;
                            temp1.Blue = CCatFrom.Blue;
                            temp1.BrightBlack = CCatFrom.BrightBlack;
                            temp1.BrightBlue = CCatFrom.BrightBlue;
                            temp1.BrightCyan = CCatFrom.BrightCyan;
                            temp1.BrightGreen = CCatFrom.BrightGreen;
                            temp1.BrightPurple = CCatFrom.BrightPurple;
                            temp1.BrightRed = CCatFrom.BrightRed;
                            temp1.BrightWhite = CCatFrom.BrightWhite;
                            temp1.BrightYellow = CCatFrom.BrightYellow;
                            temp1.CursorColor = CCatFrom.CursorColor;
                            temp1.Cyan = CCatFrom.Cyan;
                            temp1.Foreground = CCatFrom.Foreground;
                            temp1.Green = CCatFrom.Green;
                            temp1.Purple = CCatFrom.Purple;
                            temp1.Red = CCatFrom.Red;
                            temp1.SelectionBackground = CCatFrom.SelectionBackground;
                            temp1.White = CCatFrom.White;
                            temp1.Yellow = CCatFrom.Yellow;

                            TerBackground.BackColor = temp1.Background;
                            TerForeground.BackColor = temp1.Foreground;
                            TerSelection.BackColor = temp1.SelectionBackground;
                            TerCursor.BackColor = temp1.CursorColor;

                            TerBlack.BackColor = temp1.Black;
                            TerBlue.BackColor = temp1.Blue;
                            TerGreen.BackColor = temp1.Green;
                            TerCyan.BackColor = temp1.Cyan;
                            TerRed.BackColor = temp1.Red;
                            TerPurple.BackColor = temp1.Purple;
                            TerYellow.BackColor = temp1.Yellow;
                            TerWhite.BackColor = temp1.White;

                            TerBlackB.BackColor = temp1.BrightBlack;
                            TerBlueB.BackColor = temp1.BrightBlue;
                            TerGreenB.BackColor = temp1.BrightGreen;
                            TerCyanB.BackColor = temp1.BrightCyan;
                            TerRedB.BackColor = temp1.BrightRed;
                            TerPurpleB.BackColor = temp1.BrightPurple;
                            TerYellowB.BackColor = temp1.BrightYellow;
                            TerWhiteB.BackColor = temp1.BrightWhite;
                        }

                        ApplyPreview(_Terminal);

                        break;
                    }

                }

            }

        }

        private void Button21_Click(object sender, EventArgs e)
        {
            if (TerThemes.SelectedIndex < 3)
            {
                MsgBox(Program.Lang.Terminal_ThemeNotCloneable, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            {
                UI.WP.ComboBox temp = Forms.WindowsTerminalCopycat.ComboBox1;
                temp.Items.Clear();
                CCat = null;

                foreach (object x in TerThemes.Items)
                    temp.Items.Add(x);
            }

            if (Forms.WindowsTerminalCopycat.ShowDialog() == DialogResult.OK)
            {
                for (int x = 0, loopTo = TerThemes.Items.Count - 1; x <= loopTo; x++)
                {
                    if ((TerThemes.Items[x].ToString().ToLower() ?? string.Empty) == (CCat.ToLower() ?? string.Empty))
                    {

                        TTheme CCatFrom = _Terminal.Themes[x - 3];

                        {
                            TTheme temp1 = _Terminal.Themes[TerThemes.SelectedIndex - 3];
                            temp1.Style = CCatFrom.Style;
                            temp1.Tab_Active = CCatFrom.Tab_Active;
                            temp1.Tab_Inactive = CCatFrom.Tab_Inactive;
                            temp1.Titlebar_Active = CCatFrom.Titlebar_Active;
                            temp1.Titlebar_Inactive = CCatFrom.Titlebar_Inactive;
                        }

                        TerTitlebarActive.BackColor = CCatFrom.Titlebar_Active;
                        TerTitlebarInactive.BackColor = CCatFrom.Titlebar_Inactive;
                        TerTabActive.BackColor = CCatFrom.Tab_Active;
                        TerTabInactive.BackColor = CCatFrom.Tab_Inactive;
                        TerMode.Checked = !(CCatFrom.Style.ToLower() == "light");

                        break;
                    }

                }

                ApplyPreview(_Terminal);
            }
        }

        private void Button22_Click(object sender, EventArgs e)
        {
            Theme.Manager TMx = new(Theme.Manager.Source.Registry);

            switch (_Mode)
            {
                case WinTerminal.Version.Stable:
                    {
                        _Terminal = TMx.Terminal;
                        TerEnabled.Checked = TMx.Terminal.Enabled;
                        break;
                    }

                case WinTerminal.Version.Preview:
                    {
                        _Terminal = TMx.TerminalPreview;
                        TerEnabled.Checked = TMx.TerminalPreview.Enabled;
                        break;
                    }
            }

            Load_FromTerminal();

            TMx.Dispose();
        }

        private void TerFontSizeVal_Click(object sender, EventArgs e)
        {
            string response = InputBox(Program.Lang.InputValue, ((UI.WP.Button)sender).Text, Program.Lang.ItMustBeNumerical);
            ((UI.WP.Button)sender).Text = Math.Max(Math.Min(Conversion.Val(response), TerFontSizeBar.Maximum), TerFontSizeBar.Minimum).ToString();
            TerFontSizeBar.Value = (int)Math.Round(Conversion.Val(((UI.WP.Button)sender).Text));
        }

        private void TerCursorHeightVal_Click(object sender, EventArgs e)
        {
            string response = InputBox(Program.Lang.InputValue, ((UI.WP.Button)sender).Text, Program.Lang.ItMustBeNumerical);
            ((UI.WP.Button)sender).Text = Math.Max(Math.Min(Conversion.Val(response), TerCursorHeightBar.Maximum), TerCursorHeightBar.Minimum).ToString();
            TerCursorHeightBar.Value = (int)Math.Round(Conversion.Val(((UI.WP.Button)sender).Text));
        }

        private void TerImageOpacityVal_Click(object sender, EventArgs e)
        {
            string response = InputBox(Program.Lang.InputValue, ((UI.WP.Button)sender).Text, Program.Lang.ItMustBeNumerical);
            ((UI.WP.Button)sender).Text = Math.Max(Math.Min(Conversion.Val(response), TerImageOpacity.Maximum), TerImageOpacity.Minimum).ToString();
            TerImageOpacity.Value = (int)Math.Round(Conversion.Val(((UI.WP.Button)sender).Text));
        }

        private void TerOpacityVal_Click(object sender, EventArgs e)
        {
            string response = InputBox(Program.Lang.InputValue, ((UI.WP.Button)sender).Text, Program.Lang.ItMustBeNumerical);
            ((UI.WP.Button)sender).Text = Math.Max(Math.Min(Conversion.Val(response), TerOpacityBar.Maximum), TerOpacityBar.Minimum).ToString();
            TerOpacityBar.Value = (int)Math.Round(Conversion.Val(((UI.WP.Button)sender).Text));
        }

        private void TerEnabled_CheckedChanged(object sender, EventArgs e)
        {
            checker_img.Image = ((UI.WP.Toggle)sender).Checked ? Properties.Resources.checker_enabled : Properties.Resources.checker_disabled;
        }

        private void Button23_Click(object sender, EventArgs e)
        {
            FontDialog1.FixedPitchOnly = !Program.Settings.WindowsTerminals.ListAllFonts;
            FontDialog1.Font = Terminal1.Font;
            if (FontDialog1.ShowDialog() == DialogResult.OK)
            {
                TerFontName.Text = FontDialog1.Font.Name;
                NativeMethods.GDI32.LogFont fx = new();
                FontDialog1.Font.ToLogFont(fx);
                fx.lfWeight = TerFontWeight.SelectedIndex * 100;
                {
                    Font temp = Font.FromLogFont(fx);
                    Terminal1.Font = new(FontDialog1.Font.Name, FontDialog1.Font.Size, temp.Style);
                }
                TerFontName.Font = new(FontDialog1.Font.Name, 9f, Terminal1.Font.Style);
                TerFontSizeBar.Value = (int)Math.Round(FontDialog1.Font.Size);
            }

        }

        private void Form_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            Process.Start($"{Properties.Resources.Link_Wiki}/Edit-Windows-Terminals-(Windows-10-and-later)");
        }
    }
}
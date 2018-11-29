using System;
using UnityEngine;
using System.Collections;

public class InputReader
{
    public static void setSkillInputs (Player p)
    {
        string nm, readSkill;
        Skill skl;
        int chargeTime = 5;
        for (int i = 0; i < p.AllSkills.Count; i++)
        {
            skl = (Skill)p.AllSkills[i];
            readSkill = skl.Inputs[0];

            if (skl.Inputs[0].StartsWith("Jump"))
            {
                skl.TranslatedInputs.Add("AIRBORNE");
                readSkill = readSkill.Substring(4);
            }
            if (skl.Inputs[0].StartsWith("Crouch"))
            {
                skl.TranslatedInputs.Add("CROUCHING");
                readSkill = readSkill.Substring(6);
            }
            if (skl.Inputs[0].StartsWith("Rising"))
            {
                skl.TranslatedInputs.Add("RISING");
                readSkill = readSkill.Substring(6);
            }
            if (skl.Inputs[0].StartsWith("Ground"))
            {
                skl.TranslatedInputs.Add("GROUND");
                readSkill = readSkill.Substring(6);
            }
            if (skl.Inputs[0].StartsWith("FRAME>"))
            {
                skl.TranslatedInputs.Add(skl.Inputs[0].Substring(0, 13));
                readSkill = readSkill.Substring(13);
            }

            /////
            /// 
            /// 
            //UP
            if (readSkill.Equals("Up"))
            {
                skl.TranslatedInputs.Add("Up");
            }

            //BACK
            if (readSkill.Equals("Back"))
            {
                skl.TranslatedInputs.Add("Back");
            }

            //DOWN
            if (readSkill.Equals("Down"))
            {
                skl.TranslatedInputs.Add("Down");
            }

            //A
            if (readSkill.Equals("A"))
            {
                skl.TranslatedInputs.Add("A");
            }

            //B
            if (readSkill.Equals("B"))
            {
                skl.TranslatedInputs.Add("B");
            }

            //C
            if (readSkill.Equals("C"))
            {
                skl.TranslatedInputs.Add("C");
            }

            //D
            if (readSkill.Equals("D"))
            {
                skl.TranslatedInputs.Add("D");
            }

            //Y
            if (readSkill.Equals("Y"))
            {
                skl.TranslatedInputs.Add("Y");
            }

            //Z
            if (readSkill.Equals("Z"))
            {
                skl.TranslatedInputs.Add("Z");
            }

            //AB
            if (readSkill.Equals("AB"))
            {
                skl.TranslatedInputs.Add("AB");
            }
            //AC
            if (readSkill.Equals("AC"))
            {
                skl.TranslatedInputs.Add("AC");
            }
            //BD
            if (readSkill.Equals("BD"))
            {
                skl.TranslatedInputs.Add("BD");
            }
            //CD
            if (readSkill.Equals("CD"))
            {
                skl.TranslatedInputs.Add("CD");
            }
            //YZ
            if (readSkill.Equals("YZ"))
            {
                skl.TranslatedInputs.Add("YZ");
            }

            //AA
            if (readSkill.Equals("AA"))
            {
                skl.TranslatedInputs.Add("A");
                skl.TranslatedInputs.Add("A");
            }
            //BB
            if (readSkill.Equals("BB"))
            {
                skl.TranslatedInputs.Add("B");
                skl.TranslatedInputs.Add("B");
            }
            //CC
            if (readSkill.Equals("CC"))
            {
                skl.TranslatedInputs.Add("C");
                skl.TranslatedInputs.Add("C");
            }
            //DD
            if (readSkill.Equals("DD"))
            {
                skl.TranslatedInputs.Add("D");
                skl.TranslatedInputs.Add("D");
            }
            //YY
            if (readSkill.Equals("YY"))
            {
                skl.TranslatedInputs.Add("Y");
                skl.TranslatedInputs.Add("Y");
            }
            //ZZ
            if (readSkill.Equals("ZZ"))
            {
                skl.TranslatedInputs.Add("Z");
                skl.TranslatedInputs.Add("Z");
            }
            if (readSkill.Equals("ABC"))
            {
                skl.TranslatedInputs.Add("ABC");
            }


            //TWO BUTTON INPUTS
            //x2
            if (readSkill.Equals("Forwardx2"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Forward");
            }
            if (readSkill.Equals("Backx2"))
            {
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Back");
            }
            if (readSkill.Equals("Downx2"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Down");
            }

            if (readSkill.Equals("Upx2"))
            {
                skl.TranslatedInputs.Add("Up");
                skl.TranslatedInputs.Add("Up");
            }

            if (readSkill.Equals("BackForward"))
            {
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Forward");
            }


            //FORWARD
            if (readSkill.Equals("ForwardA"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("A");
            }
            if (readSkill.Equals("ForwardB"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("B");
            }
            if (readSkill.Equals("ForwardC"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("C");
            }
            if (readSkill.Equals("ForwardD"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("D");
            }
            if (readSkill.Equals("ForwardAB"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("AB");
            }
            if (readSkill.Equals("ForwardCD"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("CD");
            }
            if (readSkill.Equals("ForwardY"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Y");
            }
            if (readSkill.Equals("ForwardZ"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Z");
            }
            if (readSkill.Equals("ForwardYZ"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("YZ");
            }
            if (readSkill.Equals("ForwardCD"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("CD");
            }
            if (readSkill.Equals("ForwardAC"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("AC");
            }
            if (readSkill.Equals("ForwardBD"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("BD");
            }
            if (readSkill.Equals("ForwardBA"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("B");
                skl.TranslatedInputs.Add("A");
            }

            //FORWARD x2
            if (readSkill.Equals("Forwardx2A"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("A");
            }
            if (readSkill.Equals("Forwardx2B"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("B");
            }
            if (readSkill.Equals("Forwardx2C"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("C");
            }
            if (readSkill.Equals("Forwardx2D"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("D");
            }
            if (readSkill.Equals("Forwardx2AB"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("AB");
            }
            if (readSkill.Equals("Forwardx2CD"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("CD");
            }
            if (readSkill.Equals("Forwardx2AC"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("AC");
            }
            if (readSkill.Equals("Forwardx2BD"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("BD");
            }
            if (readSkill.Equals("Forwardx2Y"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Y");
            }
            if (readSkill.Equals("Forwardx2Z"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Z");
            }

            //BACK
            if (readSkill.Equals("BackA"))
            {
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("A");
            }
            if (readSkill.Equals("BackB"))
            {
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("B");
            }
            if (readSkill.Equals("BackC"))
            {
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("C");
            }
            if (readSkill.Equals("BackD"))
            {
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("D");
            }
            if (readSkill.Equals("BackAB"))
            {
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("AB");
            }
            if (readSkill.Equals("BackCD"))
            {
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("CD");
            }
            if (readSkill.Equals("BackAC"))
            {
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("AC");
            }
            if (readSkill.Equals("BackBD"))
            {
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("BD");
            }
            if (readSkill.Equals("BackYZ"))
            {
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("YZ");
            }
            if (readSkill.Equals("BackY"))
            {
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Y");
            }
            if (readSkill.Equals("BackZ"))
            {
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Z");
            }

            //DOWN/CROUCH
            if (readSkill.Equals("DownA"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("A");
            }
            if (readSkill.Equals("DownB"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("B");
            }
            if (readSkill.Equals("DownC"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("C");
            }
            if (readSkill.Equals("DownD"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("D");
            }
            if (readSkill.Equals("DownAB"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("AB");
            }
            if (readSkill.Equals("DownCD"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("CD");
            }
            if (readSkill.Equals("DownAC"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("AC");
            }
            if (readSkill.Equals("DownBD"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("BD");
            }
            if (readSkill.Equals("DownYZ"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("YZ");
            }
            if (readSkill.Equals("DownY"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Y");
            }
            if (readSkill.Equals("DownZ"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Z");
            }

            //Up
            if (readSkill.Equals("UpA"))
            {
                skl.TranslatedInputs.Add("Up");
                skl.TranslatedInputs.Add("A");
            }
            if (readSkill.Equals("UpB"))
            {
                skl.TranslatedInputs.Add("Up");
                skl.TranslatedInputs.Add("B");
            }
            if (readSkill.Equals("UpC"))
            {
                skl.TranslatedInputs.Add("Up");
                skl.TranslatedInputs.Add("C");
            }
            if (readSkill.Equals("UpD"))
            {
                skl.TranslatedInputs.Add("Up");
                skl.TranslatedInputs.Add("D");
            }
            if (readSkill.Equals("UpY"))
            {
                skl.TranslatedInputs.Add("Up");
                skl.TranslatedInputs.Add("Y");
            }
            if (readSkill.Equals("UpZ"))
            {
                skl.TranslatedInputs.Add("Up");
                skl.TranslatedInputs.Add("Z");
            }
            if (readSkill.Equals("UpAB"))
            {
                skl.TranslatedInputs.Add("Up");
                skl.TranslatedInputs.Add("AB");
            }
            if (readSkill.Equals("UpCD"))
            {
                skl.TranslatedInputs.Add("Up");
                skl.TranslatedInputs.Add("CD");
            }
            if (readSkill.Equals("UpAC"))
            {
                skl.TranslatedInputs.Add("Up");
                skl.TranslatedInputs.Add("AC");
            }
            if (readSkill.Equals("UpBD"))
            {
                skl.TranslatedInputs.Add("Up");
                skl.TranslatedInputs.Add("BD");
            }
            if (readSkill.Equals("BA"))
            {
                skl.TranslatedInputs.Add("B");
                skl.TranslatedInputs.Add("A");
                //skl.TranslatedInputs.Add (A);
            }

            if (readSkill.Equals("AD"))
            {
                skl.TranslatedInputs.Add("AD");
            }
            if (readSkill.Equals("BC"))
            {
                skl.TranslatedInputs.Add("BC");
            }

            if (readSkill.Equals("BackAD"))
            {
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("AD");
            }
            if (readSkill.Equals("BackBC"))
            {
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("BC");
            }


            //QCF
            if (readSkill.Equals("QCF"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
            }
            if (readSkill.Equals("CrouchAB"))
            {
                skl.TranslatedInputs.Add("AB");
            }
            if (readSkill.Equals("QCFA") || readSkill.Equals("CrouchForwardA"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("A");
            }
            if (readSkill.Equals("QCFB") || readSkill.Equals("CrouchForwardB"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("B");
            }
            if (readSkill.Equals("QCFC") || readSkill.Equals("CrouchForwardC"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("C");
            }
            if (readSkill.Equals("QCFD") || readSkill.Equals("CrouchForwardD"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("D");
            }
            if (readSkill.Equals("QCFAB") || readSkill.Equals("CrouchForwardAB"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("AB");
            }
            if (readSkill.Equals("QCFCD"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("C");
                skl.TranslatedInputs.Add("D");
            }

            if (readSkill.Equals("CrouchForwardCD"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("CD");
            }
            if (readSkill.Equals("QCFAC") || readSkill.Equals("CrouchForwardAC"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("AC");
            }
            if (readSkill.Equals("QCFBD") || readSkill.Equals("CrouchForwardBD"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("BD");
            }
            if (readSkill.Equals("QCFY") || readSkill.Equals("CrouchForwardY"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Y");
            }
            if (readSkill.Equals("QCFZ") || readSkill.Equals("CrouchForwardZ"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Z");
            }
            if (readSkill.Equals("QCFYZ") || readSkill.Equals("CrouchForwardYZ"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("YZ");
            }
            if (readSkill.Equals("QCFYY"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Y");
                skl.TranslatedInputs.Add("Y");
            }
            if (readSkill.Equals("QCFZZ"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Z");
                skl.TranslatedInputs.Add("Z");
            }

            //Down Up
            if (readSkill.Equals("DownUpA"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Up");
                skl.TranslatedInputs.Add("A");
            }
            if (readSkill.Equals("DownUpB"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Up");
                skl.TranslatedInputs.Add("B");
            }
            if (readSkill.Equals("DownUpC"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Up");
                skl.TranslatedInputs.Add("C");
            }
            if (readSkill.Equals("DownUpD"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Up");
                skl.TranslatedInputs.Add("D");
            }
            if (readSkill.Equals("DownUpY"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Up");
                skl.TranslatedInputs.Add("Y");
            }
            if (readSkill.Equals("DownUpZ"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Up");
                skl.TranslatedInputs.Add("Z");
            }
            if (readSkill.Equals("DownUpAB"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Up");
                skl.TranslatedInputs.Add("AB");
            }
            if (readSkill.Equals("DownUpCD"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Up");
                skl.TranslatedInputs.Add("CD");
            }
            if (readSkill.Equals("DownUpAC"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Up");
                skl.TranslatedInputs.Add("AC");
            }
            if (readSkill.Equals("DownUpBD"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Up");
                skl.TranslatedInputs.Add("BD");
            }
            if (readSkill.Equals("DownUpBA"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Up");
                skl.TranslatedInputs.Add("B");
                skl.TranslatedInputs.Add("A");
                //skl.TranslatedInputs.Add (A);
            }


            //QCF x2
            if (readSkill.Equals("QCFx2A"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("A");
            }
            if (readSkill.Equals("QCFx2B"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("B");
            }
            if (readSkill.Equals("QCFx2C"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("C");
            }
            if (readSkill.Equals("QCFx2D"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("D");
            }
            if (readSkill.Equals("QCFx2Y"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Y");
            }
            if (readSkill.Equals("QCFx2Z"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Z");
            }
            if (readSkill.Equals("QCFx2AB"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("AB");
            }
            if (readSkill.Equals("QCFxCD"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("CD");
            }
            if (readSkill.Equals("QCFx2AC"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("AC");
            }
            if (readSkill.Equals("QCFx2BD"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("BD");
            }
            if (readSkill.Equals("QCFx2YZ"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("YZ");
            }
            if (readSkill.Equals("QCFx4Y"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Y");
            }

            //QCF x2
            if (readSkill.Equals("QCBx2A"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("A");
            }
            if (readSkill.Equals("QCBx2B"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("B");
            }
            if (readSkill.Equals("QCBx2C"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("C");
            }
            if (readSkill.Equals("QCBx2D"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("D");
            }
            if (readSkill.Equals("QCBx2Y"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Y");
            }
            if (readSkill.Equals("QCBx2Z"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Z");
            }
            if (readSkill.Equals("QCBx2AB"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("AB");
            }
            if (readSkill.Equals("QCBx2CD"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("CD");
            }
            if (readSkill.Equals("QCBx2AC"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("AC");
            }
            if (readSkill.Equals("QCBx2BD"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("BD");
            }
            if (readSkill.Equals("QCBx2YZ"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("YZ");
            }

            //QCB
            if (readSkill.Equals("QCB"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
            }
            if (readSkill.Equals("QCBA"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("A");
            }
            if (readSkill.Equals("QCBB"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("B");
            }
            if (readSkill.Equals("QCBC"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("C");
            }
            if (readSkill.Equals("QCBD"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("D");
            }
            if (readSkill.Equals("QCBY"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Y");
            }
            if (readSkill.Equals("QCBZ"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Z");
            }
            if (readSkill.Equals("QCBAB"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("AB");
            }
            if (readSkill.Equals("QCBCD"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("CD");
            }
            if (readSkill.Equals("QCBAC"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("AC");
            }
            if (readSkill.Equals("QCBBD"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("BD");
            }
            if (readSkill.Equals("QCBYZ"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("YZ");
            }

            //QCB
            if (readSkill.Equals("QCDA"))
            {
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("A");
            }
            if (readSkill.Equals("QCDB"))
            {
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("B");
            }
            if (readSkill.Equals("QCDC"))
            {
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("C");
            }
            if (readSkill.Equals("QCDD"))
            {
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("D");
            }
            if (readSkill.Equals("QCDY"))
            {
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Y");
            }
            if (readSkill.Equals("QCDZ"))
            {
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Z");
            }
            if (readSkill.Equals("QCDAB"))
            {
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("AB");
            }
            if (readSkill.Equals("QCDCD"))
            {
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("CD");
            }
            if (readSkill.Equals("QCDAC"))
            {
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("AC");
            }
            if (readSkill.Equals("QCDBD"))
            {
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("BD");
            }
            if (readSkill.Equals("QCDYZ"))
            {
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("YZ");
            }

            //Down Down
            if (readSkill.Equals("Downx2A"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("A");
            }
            if (readSkill.Equals("Downx2B"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("B");
            }
            if (readSkill.Equals("Downx2C"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("C");
            }
            if (readSkill.Equals("Downx2D"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("D");
            }
            if (readSkill.Equals("Downx2Y"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Y");
            }
            if (readSkill.Equals("Downx2Z"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Z");
            }
            if (readSkill.Equals("Downx2AB"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("AB");
            }
            if (readSkill.Equals("Downx2CD"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("CD");
            }
            if (readSkill.Equals("Downx2AC"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("AC");
            }
            if (readSkill.Equals("Downx2BD"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("BD");
            }
            if (readSkill.Equals("Downx2YZ"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("YZ");
            }

            //Down Down Down
            if (readSkill.Equals("Downx3A"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("A");
            }
            if (readSkill.Equals("Downx3B"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("B");
            }
            if (readSkill.Equals("Downx3C"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("C");
            }
            if (readSkill.Equals("Downx3D"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("D");
            }
            if (readSkill.Equals("Downx3Y"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Y");
            }
            if (readSkill.Equals("Downx3Z"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Z");
            }
            if (readSkill.Equals("Downx3AB"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("AB");
            }
            if (readSkill.Equals("Downx3CD"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("CD");
            }
            if (readSkill.Equals("Downx3AC"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("AC");
            }
            if (readSkill.Equals("Downx3BD"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("BD");
            }
            if (readSkill.Equals("Downx3YZ"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("YZ");
            }

            //DP
            if (readSkill.Equals("DP"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
            }
            if (readSkill.Equals("DPA"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("A");
            }
            if (readSkill.Equals("DPB"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("B");
            }
            if (readSkill.Equals("DPC"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("C");
            }
            if (readSkill.Equals("DPD"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("D");
            }
            if (readSkill.Equals("DPY"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Y");
            }
            if (readSkill.Equals("DPZ"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Z");
            }
            if (readSkill.Equals("DPAB"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("AB");
            }
            if (readSkill.Equals("DPCD"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("CD");
            }
            if (readSkill.Equals("DPAC"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("AC");
            }
            if (readSkill.Equals("DPBD"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("BD");
            }
            if (readSkill.Equals("DPYZ"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("YZ");
            }

            //FORWARD x3
            if (readSkill.Equals("Forwardx3A"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("A");
            }
            if (readSkill.Equals("Forwardx3B"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("B");
            }
            if (readSkill.Equals("Forwardx3C"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("C");
            }
            if (readSkill.Equals("Forwardx3D"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("D");
            }
            if (readSkill.Equals("Forwardx3AB"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("AB");
            }
            if (readSkill.Equals("Forwardx3CD"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("CD");
            }
            if (readSkill.Equals("Forwardx3AC"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("AC");
            }
            if (readSkill.Equals("Forwardx3BD"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("BD");
            }
            if (readSkill.Equals("Forwardx3Y"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Y");
            }
            if (readSkill.Equals("Forwardx3Z"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Z");
            }
            if (readSkill.Equals("Forwardx3YZ"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("YZ");
            }

            //QCB Forward
            if (readSkill.Equals("QCBForwardA"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("A");
            }
            if (readSkill.Equals("QCBForwardB"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("B");
            }
            if (readSkill.Equals("QCBForwardC"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("C");
            }
            if (readSkill.Equals("QCBForwardD"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("D");
            }
            if (readSkill.Equals("QCBForwardY"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Y");
            }
            if (readSkill.Equals("QCBForwardZ"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Z");
            }
            if (readSkill.Equals("QCBForwardAB"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("AB");
            }
            if (readSkill.Equals("QCBForwardCD"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("CD");
            }
            if (readSkill.Equals("QCBForwardAC"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("AC");
            }
            if (readSkill.Equals("QCBForwardBD"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("BD");
            }
            if (readSkill.Equals("QCBForwardYZ"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("YZ");
            }

            //RDP
            if (readSkill.Equals("RDP"))
            {
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
            }
            if (readSkill.Equals("RDPA"))
            {
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("A");
            }
            if (readSkill.Equals("RDPB"))
            {
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("B");
            }
            if (readSkill.Equals("RDPC"))
            {
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("C");
            }
            if (readSkill.Equals("RDPD"))
            {
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("D");
            }
            if (readSkill.Equals("RDPY"))
            {
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Y");
            }
            if (readSkill.Equals("RDPZ"))
            {
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Z");
            }
            if (readSkill.Equals("RDPAB"))
            {
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("AB");
            }
            if (readSkill.Equals("RDPCD"))
            {
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("CD");
            }
            if (readSkill.Equals("RDPAC"))
            {
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("AC");
            }
            if (readSkill.Equals("RDPBD"))
            {
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("BD");
            }
            if (readSkill.Equals("RDPYZ"))
            {
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("YZ");
            }

            //HCF
            if (readSkill.Equals("HCF"))
            {
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
            }
            if (readSkill.Equals("HCFA"))
            {
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("A");
            }
            if (readSkill.Equals("HCFB"))
            {
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("B");
            }
            if (readSkill.Equals("HCFC"))
            {
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("C");
            }
            if (readSkill.Equals("HCFD"))
            {
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("D");
            }
            if (readSkill.Equals("HCFY"))
            {
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Y");
            }
            if (readSkill.Equals("HCFZ"))
            {
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Z");
            }
            if (readSkill.Equals("HCFAB"))
            {
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("AB");
            }
            if (readSkill.Equals("HCFCD"))
            {
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("CD");
            }
            if (readSkill.Equals("HCFAC"))
            {
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("AC");
            }
            if (readSkill.Equals("HCFBD"))
            {
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("BD");
            }
            if (readSkill.Equals("HCFYZ"))
            {
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("YZ");
            }

            //HCF x2
            if (readSkill.Equals("HCFx2A"))
            {
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("A");
            }
            if (readSkill.Equals("HCFx2B"))
            {
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("B");
            }
            if (readSkill.Equals("HCFCx2"))
            {
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("C");
            }
            if (readSkill.Equals("HCFx2D"))
            {
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("D");
            }
            if (readSkill.Equals("HCFx2Y"))
            {
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Y");
            }
            if (readSkill.Equals("HCFx2Z"))
            {
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Z");
            }
            if (readSkill.Equals("HCFx2AB"))
            {
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("AB");
            }
            if (readSkill.Equals("HCFx2CD"))
            {
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("CD");
            }
            if (readSkill.Equals("HCFx2AC"))
            {
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("AC");
            }
            if (readSkill.Equals("HCFx2BD"))
            {
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("BD");
            }
            if (readSkill.Equals("HCFx2YZ"))
            {
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("YZ");
            }


            //HCB
            if (readSkill.Equals("HCB"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
            }
            if (readSkill.Equals("HCBA"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("A");
            }
            if (readSkill.Equals("HCBB"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("B");
            }
            if (readSkill.Equals("HCBC"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("C");
            }
            if (readSkill.Equals("HCBD"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("D");
            }
            if (readSkill.Equals("HCBY"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Y");
            }
            if (readSkill.Equals("HCBZ"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Z");
            }
            if (readSkill.Equals("HCBAB"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("AB");
            }
            if (readSkill.Equals("HCBCD"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("CD");
            }
            if (readSkill.Equals("HCBAC"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("AC");
            }
            if (readSkill.Equals("HCBBD"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("BD");
            }
            if (readSkill.Equals("HCBYZ"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("YZ");
            }

            //HCBx2
            if (readSkill.Equals("HCBx2A"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("A");
            }
            if (readSkill.Equals("HCBx2B"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("B");
            }
            if (readSkill.Equals("HCBx2C"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("C");
            }
            if (readSkill.Equals("HCBx2D"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("D");
            }
            if (readSkill.Equals("HCBx2Y"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Y");
            }
            if (readSkill.Equals("HCBx2Z"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Z");
            }
            if (readSkill.Equals("HCBx2AB"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("AB");
            }
            if (readSkill.Equals("HCBx2CD"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("CD");
            }
            if (readSkill.Equals("HCBx2AC"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("AC");
            }
            if (readSkill.Equals("HCBx2BD"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("BD");
            }
            if (readSkill.Equals("HCBx2YZ"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("YZ");
            }


            //Forward HCF
            if (readSkill.Equals("ForwardHCFA"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("A");
            }
            if (readSkill.Equals("ForwardHCFB"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("B");
            }
            if (readSkill.Equals("ForwardHCFC"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("C");
            }
            if (readSkill.Equals("ForwardHCFD"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("D");
            }
            if (readSkill.Equals("ForwardHCFY"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Y");
            }
            if (readSkill.Equals("ForwardHCFZ"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Z");
            }
            if (readSkill.Equals("ForwardHCFAB"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("AB");
            }
            if (readSkill.Equals("ForwardHCFCD"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("CD");
            }
            if (readSkill.Equals("ForwardHCFAC"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("AC");
            }
            if (readSkill.Equals("ForwardHCFBD"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("BD");
            }
            if (readSkill.Equals("ForwardHCFYZ"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("YZ");
            }


            //HCB Forward
            if (readSkill.Equals("HCBForwardA"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("A");
            }
            if (readSkill.Equals("HCBForwardB"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("B");
            }
            if (readSkill.Equals("HCBForwardC"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("C");
            }
            if (readSkill.Equals("HCBForwardD"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("D");
            }
            if (readSkill.Equals("HCBForwardY"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Y");
            }
            if (readSkill.Equals("HCBForwardZ"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Z");
            }
            if (readSkill.Equals("HCBForwardAB"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("AB");
            }
            if (readSkill.Equals("HCBForwardCD"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("CD");
            }
            if (readSkill.Equals("HCBForwardAC"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("AC");
            }
            if (readSkill.Equals("HCBForwardBD"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("BD");
            }
            if (readSkill.Equals("HCBForwardYZ"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("YZ");
            }

            //QCFQCB
            if (readSkill.Equals("QCFHCBA"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("A");
            }
            if (readSkill.Equals("QCFHCBB"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("B");
            }
            if (readSkill.Equals("QCFHCBC"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("C");
            }
            if (readSkill.Equals("QCFHCBD"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("D");
            }
            if (readSkill.Equals("QCFHCBY"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Y");
            }
            if (readSkill.Equals("QCFHCBZ"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Z");
            }
            if (readSkill.Equals("QCFHCBAB"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("AB");
            }
            if (readSkill.Equals("QCFHCBCD"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("CD");
            }
            if (readSkill.Equals("QCFHCBAC"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("AC");
            }
            if (readSkill.Equals("QCFHCBBD"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("BD");
            }
            if (readSkill.Equals("QCFHCBYZ"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("YZ");
            }

            //QCBQCF
            if (readSkill.Equals("QCBHCFA"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("A");
            }
            if (readSkill.Equals("QCBHCFB"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("B");
            }
            if (readSkill.Equals("QCBHCFC"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("C");
            }
            if (readSkill.Equals("QCBHCFB"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("D");
            }
            if (readSkill.Equals("QCBHCFY"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Y");
            }
            if (readSkill.Equals("QCBHCFZ"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Z");
            }
            if (readSkill.Equals("QCBHCFAB"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("AB");
            }
            if (readSkill.Equals("QCBHCFCD"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("CD");
            }
            if (readSkill.Equals("QCBHCFAC"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("AC");
            }
            if (readSkill.Equals("QCBHCFBD"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("BD");
            }
            if (readSkill.Equals("QCBHCFYZ"))
            {
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("YZ");
            }

            //BACK FORWARD
            if (readSkill.Equals("BackChargeForward"))
            {
                for (int j = 0; j < chargeTime; j++)
                {
                    skl.TranslatedInputs.Add("Back");
                }
                skl.TranslatedInputs.Add("Forward");
            }
            if (readSkill.Equals("BackChargeForwardA"))
            {
                for (int j = 0; j < chargeTime; j++)
                {
                    skl.TranslatedInputs.Add("Back");
                }
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("A");
            }
            if (readSkill.Equals("BackChargeForwardB"))
            {
                for (int j = 0; j < chargeTime; j++)
                {
                    skl.TranslatedInputs.Add("Back");
                }
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("B");
            }
            if (readSkill.Equals("BackChargeForwardC"))
            {
                for (int j = 0; j < chargeTime; j++)
                {
                    skl.TranslatedInputs.Add("Back");
                }
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("C");
            }
            if (readSkill.Equals("BackChargeForwardD"))
            {
                for (int j = 0; j < chargeTime; j++)
                {
                    skl.TranslatedInputs.Add("Back");
                }
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("D");
            }
            if (readSkill.Equals("BackChargeForwardY"))
            {
                for (int j = 0; j < chargeTime; j++)
                {
                    skl.TranslatedInputs.Add("Back");
                }
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Y");
            }
            if (readSkill.Equals("BackChargeForwardZ"))
            {
                for (int j = 0; j < chargeTime; j++)
                {
                    skl.TranslatedInputs.Add("Back");
                }
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Z");
            }
            if (readSkill.Equals("BackChargeForwardAB"))
            {
                for (int j = 0; j < chargeTime; j++)
                {
                    skl.TranslatedInputs.Add("Back");
                }
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("AB");
            }
            if (readSkill.Equals("BackChargeForwardCD"))
            {
                for (int j = 0; j < chargeTime; j++)
                {
                    skl.TranslatedInputs.Add("Back");
                }
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("CD");
            }
            if (readSkill.Equals("BackChargeForwardAC"))
            {
                for (int j = 0; j < chargeTime; j++)
                {
                    skl.TranslatedInputs.Add("Back");
                }
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("AC");
            }
            if (readSkill.Equals("BackChargeForwardBD"))
            {
                for (int j = 0; j < chargeTime; j++)
                {
                    skl.TranslatedInputs.Add("Back");
                }
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("BD");
            }
            if (readSkill.Equals("BackChargeForwardYZ"))
            {
                for (int j = 0; j < chargeTime; j++)
                {
                    skl.TranslatedInputs.Add("Back");
                }
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("YZ");
            }
            if (readSkill.Equals("BackChargeForwardAA"))
            {
                for (int j = 0; j < chargeTime; j++)
                {
                    skl.TranslatedInputs.Add("Back");
                }
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("A");
                skl.TranslatedInputs.Add("A");
            }


            //BACK FORWARD-BACK-FORWARD
            if (readSkill.Equals("BackChargeFBFA"))
            {
                for (int j = 0; j < chargeTime; j++)
                {
                    skl.TranslatedInputs.Add("Back");
                }
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("A");
            }
            if (readSkill.Equals("BackChargeFBFB"))
            {
                for (int j = 0; j < chargeTime; j++)
                {
                    skl.TranslatedInputs.Add("Back");
                }
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("B");
            }
            if (readSkill.Equals("BackChargeFBFC"))
            {
                for (int j = 0; j < chargeTime; j++)
                {
                    skl.TranslatedInputs.Add("Back");
                }
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("C");
            }
            if (readSkill.Equals("BackChargeFBFD"))
            {
                for (int j = 0; j < chargeTime; j++)
                {
                    skl.TranslatedInputs.Add("Back");
                }
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("D");
            }
            if (readSkill.Equals("BackChargeFBFY"))
            {
                for (int j = 0; j < chargeTime; j++)
                {
                    skl.TranslatedInputs.Add("Back");
                }
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Y");
            }
            if (readSkill.Equals("BackChargeFBFZ"))
            {
                for (int j = 0; j < chargeTime; j++)
                {
                    skl.TranslatedInputs.Add("Back");
                }
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Z");
            }
            if (readSkill.Equals("BackChargeFBFAB"))
            {
                for (int j = 0; j < chargeTime; j++)
                {
                    skl.TranslatedInputs.Add("Back");
                }
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("AB");
            }
            if (readSkill.Equals("BackChargeFBFCD"))
            {
                for (int j = 0; j < chargeTime; j++)
                {
                    skl.TranslatedInputs.Add("Back");
                }
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("CD");
            }
            if (readSkill.Equals("BackChargeFBFAC"))
            {
                for (int j = 0; j < chargeTime; j++)
                {
                    skl.TranslatedInputs.Add("Back");
                }
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("AC");
            }
            if (readSkill.Equals("BackChargeFBFBD"))
            {
                for (int j = 0; j < chargeTime; j++)
                {
                    skl.TranslatedInputs.Add("Back");
                }
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("BD");
            }
            if (readSkill.Equals("BackChargeFBFYZ"))
            {
                for (int j = 0; j < chargeTime; j++)
                {
                    skl.TranslatedInputs.Add("Back");
                }
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("YZ");
            }

            //Down Up
            if (readSkill.Equals("DownChargeUp"))
            {
                for (int j = 0; j < chargeTime; j++)
                {
                    skl.TranslatedInputs.Add("Down");
                }
                skl.TranslatedInputs.Add("Up");
            }
            if (readSkill.Equals("DownChargeUpA"))
            {
                for (int j = 0; j < chargeTime; j++)
                {
                    skl.TranslatedInputs.Add("Down");
                }
                skl.TranslatedInputs.Add("Up");
                skl.TranslatedInputs.Add("A");
            }
            if (readSkill.Equals("DownChargeUpB"))
            {
                for (int j = 0; j < chargeTime; j++)
                {
                    skl.TranslatedInputs.Add("Down");
                }
                skl.TranslatedInputs.Add("Up");
                skl.TranslatedInputs.Add("B");
            }
            if (readSkill.Equals("DownChargeUpC"))
            {
                for (int j = 0; j < chargeTime; j++)
                {
                    skl.TranslatedInputs.Add("Down");
                }
                skl.TranslatedInputs.Add("Up");
                skl.TranslatedInputs.Add("C");
            }
            if (readSkill.Equals("DownChargeUpD"))
            {
                for (int j = 0; j < chargeTime; j++)
                {
                    skl.TranslatedInputs.Add("Down");
                }
                skl.TranslatedInputs.Add("Up");
                skl.TranslatedInputs.Add("D");
            }
            if (readSkill.Equals("DownChargeUpY"))
            {
                for (int j = 0; j < chargeTime; j++)
                {
                    skl.TranslatedInputs.Add("Down");
                }
                skl.TranslatedInputs.Add("Up");
                skl.TranslatedInputs.Add("Y");
            }
            if (readSkill.Equals("DownChargeUpZ"))
            {
                for (int j = 0; j < chargeTime; j++)
                {
                    skl.TranslatedInputs.Add("Down");
                }
                skl.TranslatedInputs.Add("Up");
                skl.TranslatedInputs.Add("Z");
            }
            if (readSkill.Equals("DownChargeUpAB"))
            {
                for (int j = 0; j < chargeTime; j++)
                {
                    skl.TranslatedInputs.Add("Down");
                }
                skl.TranslatedInputs.Add("Up");
                skl.TranslatedInputs.Add("AB");
            }
            if (readSkill.Equals("DownChargeUpCD"))
            {
                for (int j = 0; j < chargeTime; j++)
                {
                    skl.TranslatedInputs.Add("Down");
                }
                skl.TranslatedInputs.Add("Up");
                skl.TranslatedInputs.Add("CD");
            }
            if (readSkill.Equals("DownChargeUpAC"))
            {
                for (int j = 0; j < chargeTime; j++)
                {
                    skl.TranslatedInputs.Add("Down");
                }
                skl.TranslatedInputs.Add("Up");
                skl.TranslatedInputs.Add("AC");
            }
            if (readSkill.Equals("DownChargeUpBD"))
            {
                for (int j = 0; j < chargeTime; j++)
                {
                    skl.TranslatedInputs.Add("Down");
                }
                skl.TranslatedInputs.Add("Up");
                skl.TranslatedInputs.Add("BD");
            }
            if (readSkill.Equals("DownChargeUpYZ"))
            {
                for (int j = 0; j < chargeTime; j++)
                {
                    skl.TranslatedInputs.Add("Down");
                }
                skl.TranslatedInputs.Add("Up");
                skl.TranslatedInputs.Add("YZ");
            }

            //Down Up
            if (readSkill.Equals("DownChargeFDUA"))
            {
                for (int j = 0; j < chargeTime; j++)
                {
                    skl.TranslatedInputs.Add("Down");
                }
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Up");
                skl.TranslatedInputs.Add("A");
            }
            if (readSkill.Equals("DownChargeFDUB"))
            {
                for (int j = 0; j < chargeTime; j++)
                {
                    skl.TranslatedInputs.Add("Down");
                }
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Up");
                skl.TranslatedInputs.Add("B");
            }
            if (readSkill.Equals("DownChargeFDUC"))
            {
                for (int j = 0; j < chargeTime; j++)
                {
                    skl.TranslatedInputs.Add("Down");
                }
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Up");
                skl.TranslatedInputs.Add("C");
            }
            if (readSkill.Equals("DownChargeFDUD"))
            {
                for (int j = 0; j < chargeTime; j++)
                {
                    skl.TranslatedInputs.Add("Down");
                }
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Up");
                skl.TranslatedInputs.Add("D");
            }
            if (readSkill.Equals("DownChargeFDUY"))
            {
                for (int j = 0; j < chargeTime; j++)
                {
                    skl.TranslatedInputs.Add("Down");
                }
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Up");
                skl.TranslatedInputs.Add("Y");
            }
            if (readSkill.Equals("DownChargeFDUZ"))
            {
                for (int j = 0; j < chargeTime; j++)
                {
                    skl.TranslatedInputs.Add("Down");
                }
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Up");
                skl.TranslatedInputs.Add("Z");
            }
            if (readSkill.Equals("DownChargeFDUAB"))
            {
                for (int j = 0; j < chargeTime; j++)
                {
                    skl.TranslatedInputs.Add("Down");
                }
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Up");
                skl.TranslatedInputs.Add("AB");
            }
            if (readSkill.Equals("DownChargeFDUCD"))
            {
                for (int j = 0; j < chargeTime; j++)
                {
                    skl.TranslatedInputs.Add("Down");
                }
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Up");
                skl.TranslatedInputs.Add("CD");
            }
            if (readSkill.Equals("DownChargeFDUAC"))
            {
                for (int j = 0; j < chargeTime; j++)
                {
                    skl.TranslatedInputs.Add("Down");
                }
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Up");
                skl.TranslatedInputs.Add("AC");
            }
            if (readSkill.Equals("DownChargeFDUBD"))
            {
                for (int j = 0; j < chargeTime; j++)
                {
                    skl.TranslatedInputs.Add("Down");
                }
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Up");
                skl.TranslatedInputs.Add("BD");
            }
            if (readSkill.Equals("DownChargeFDUYZ"))
            {
                for (int j = 0; j < chargeTime; j++)
                {
                    skl.TranslatedInputs.Add("Down");
                }
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Up");
                skl.TranslatedInputs.Add("YZ");
            }

            //Down Up
            if (readSkill.Equals("FBF"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Forward");
            }
            if (readSkill.Equals("FBFA"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("A");
            }
            if (readSkill.Equals("FBFB"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("B");
            }
            if (readSkill.Equals("FBFC"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("C");
            }
            if (readSkill.Equals("FBFD"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("D");
            }
            if (readSkill.Equals("FBFY"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Y");
            }
            if (readSkill.Equals("FBFZ"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Z");
            }
            if (readSkill.Equals("FBFAB"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("AB");
            }
            if (readSkill.Equals("FBFCD"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("CD");
            }
            if (readSkill.Equals("FBFAC"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("AC");
            }
            if (readSkill.Equals("FBFBD"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("BD");
            }
            if (readSkill.Equals("FBFYZ"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("YZ");
            }

            //360
            if (readSkill.Equals("360"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Up");
            }
            if (readSkill.Equals("360A"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Up");
                skl.TranslatedInputs.Add("A");
            }
            if (readSkill.Equals("360B"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Up");
                skl.TranslatedInputs.Add("B");
            }
            if (readSkill.Equals("360C"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Up");
                skl.TranslatedInputs.Add("C");
            }
            if (readSkill.Equals("360D"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Up");
                skl.TranslatedInputs.Add("D");
            }
            if (readSkill.Equals("360Y"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Up");
                skl.TranslatedInputs.Add("Y");
            }
            if (readSkill.Equals("360Z"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Up");
                skl.TranslatedInputs.Add("Z");
            }
            if (readSkill.Equals("360AB"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Up");
                skl.TranslatedInputs.Add("AB");
            }
            if (readSkill.Equals("360CD"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Up");
                skl.TranslatedInputs.Add("CD");
            }
            if (readSkill.Equals("360AC"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Up");
                skl.TranslatedInputs.Add("AC");
            }
            if (readSkill.Equals("360BD"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Up");
                skl.TranslatedInputs.Add("BD");
            }
            if (readSkill.Equals("360YZ"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Up");
                skl.TranslatedInputs.Add("YZ");
            }

            //720
            if (readSkill.Equals("720A"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Up");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Up");
                skl.TranslatedInputs.Add("A");
            }
            if (readSkill.Equals("720B"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Up");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Up");
                skl.TranslatedInputs.Add("B");
            }
            if (readSkill.Equals("720C"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Up");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Up");
                skl.TranslatedInputs.Add("C");
            }
            if (readSkill.Equals("720D"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Up");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Up");
                skl.TranslatedInputs.Add("D");
            }
            if (readSkill.Equals("720Y"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Up");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Up");
                skl.TranslatedInputs.Add("Y");
            }
            if (readSkill.Equals("720Z"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Up");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Up");
                skl.TranslatedInputs.Add("Z");
            }
            if (readSkill.Equals("720AB"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Up");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Up");
                skl.TranslatedInputs.Add("AB");
            }
            if (readSkill.Equals("720CD"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Up");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Up");
                skl.TranslatedInputs.Add("CD");
            }
            if (readSkill.Equals("720AC"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Up");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Up");
                skl.TranslatedInputs.Add("AC");
            }
            if (readSkill.Equals("720BD"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Up");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Up");
                skl.TranslatedInputs.Add("BD");
            }
            if (readSkill.Equals("720YZ"))
            {
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Up");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("Down");
                skl.TranslatedInputs.Add("Back");
                skl.TranslatedInputs.Add("Up");
                skl.TranslatedInputs.Add("YZ");
            }
            if (readSkill.Equals("RapidAB"))
            {
                skl.TranslatedInputs.Add("A");
                skl.TranslatedInputs.Add("B");
                skl.TranslatedInputs.Add("A");
                skl.TranslatedInputs.Add("B");
                skl.TranslatedInputs.Add("A");
                skl.TranslatedInputs.Add("B");
                skl.TranslatedInputs.Add("A");
                skl.TranslatedInputs.Add("B");
            }

            if (readSkill.Equals("RapidCD"))
            {
                skl.TranslatedInputs.Add("C");
                skl.TranslatedInputs.Add("D");
                skl.TranslatedInputs.Add("C");
                skl.TranslatedInputs.Add("D");
                skl.TranslatedInputs.Add("C");
                skl.TranslatedInputs.Add("D");
                skl.TranslatedInputs.Add("C");
                skl.TranslatedInputs.Add("D");
            }
            if (readSkill.Equals("RapidABCD"))
            {
                skl.TranslatedInputs.Add("AB");
                skl.TranslatedInputs.Add("CD");
                skl.TranslatedInputs.Add("AB");
                skl.TranslatedInputs.Add("CD");
                skl.TranslatedInputs.Add("AB");
                skl.TranslatedInputs.Add("CD");
                skl.TranslatedInputs.Add("AB");
                skl.TranslatedInputs.Add("CD");
            }

            if (readSkill.Equals("Ax5"))
            {
                skl.TranslatedInputs.Add("A");
                skl.TranslatedInputs.Add("A");
                skl.TranslatedInputs.Add("A");
                skl.TranslatedInputs.Add("A");
                skl.TranslatedInputs.Add("A");
            }
            if (readSkill.Equals("Bx5"))
            {
                skl.TranslatedInputs.Add("B");
                skl.TranslatedInputs.Add("B");
                skl.TranslatedInputs.Add("B");
                skl.TranslatedInputs.Add("B");
                skl.TranslatedInputs.Add("B");
            }
            if (readSkill.Equals("Cx5"))
            {
                skl.TranslatedInputs.Add("C");
                skl.TranslatedInputs.Add("C");
                skl.TranslatedInputs.Add("C");
                skl.TranslatedInputs.Add("C");
                skl.TranslatedInputs.Add("C");
            }
            if (readSkill.Equals("Dx5"))
            {
                skl.TranslatedInputs.Add("D");
                skl.TranslatedInputs.Add("D");
                skl.TranslatedInputs.Add("D");
                skl.TranslatedInputs.Add("D");
                skl.TranslatedInputs.Add("D");
            }
            if (readSkill.Equals("Yx5"))
            {
                skl.TranslatedInputs.Add("Y");
                skl.TranslatedInputs.Add("Y");
                skl.TranslatedInputs.Add("Y");
                skl.TranslatedInputs.Add("Y");
                skl.TranslatedInputs.Add("Y");
            }
            if (readSkill.Equals("Zx5"))
            {
                skl.TranslatedInputs.Add("Z");
                skl.TranslatedInputs.Add("Z");
                skl.TranslatedInputs.Add("Z");
                skl.TranslatedInputs.Add("Z");
                skl.TranslatedInputs.Add("Z");
            }
            if (readSkill.Equals("ABCDABCD"))
            {
                skl.TranslatedInputs.Add("A");
                skl.TranslatedInputs.Add("B");
                skl.TranslatedInputs.Add("C");
                skl.TranslatedInputs.Add("D");
                skl.TranslatedInputs.Add("A");
                skl.TranslatedInputs.Add("B");
                skl.TranslatedInputs.Add("C");
                skl.TranslatedInputs.Add("D");
            }
            if (readSkill.Equals("ABCDAll"))
            {
                skl.TranslatedInputs.Add("ABCD");
            }
            if (readSkill.Equals("RD"))
            {
                skl.TranslatedInputs.Add("A");
                skl.TranslatedInputs.Add("A");
                skl.TranslatedInputs.Add("Forward");
                skl.TranslatedInputs.Add("B");
                skl.TranslatedInputs.Add("Y");
            }
            UnityEngine.Debug.Log(skl.Name + ": " + skl.Inputs[0] + " (" + skl.TranslatedInputs.Count + ")");
        }
    }

    public static Boolean containsInput (string tkn, string comp, string ext, ArrayList lst)
    {
        if (tkn.Equals (comp) && lst.Contains (comp) || (lst.Contains (ext))) {
            return true;
        }
        return false;
    }

    public static Skill readInput (Player pl) {
        Skill sk = null, skl;
        ArrayList inpts;
        for (int i = 0; i < pl.NormalSkills.Count; i ++) {
        //for (int i = 0; i < pl.AllSkills.Count; i ++) {
            skl = (Skill)pl.NormalSkills[i];
            //skl = (Skill)pl.AllSkills [i];
            sk = skl;

            for (int j = 0; j < skl.TranslatedInputs.Count; j ++) {
                if (pl.Inputs.Count > j) {

                    inpts = (ArrayList)pl.Inputs [0];//[(pl.Inputs.Count - 1) - j];
                    UnityEngine.Debug.Log (pl.Index + " " + skl.Name + " "
                                          + (j + 1) + @") !!!!!!!!!!!!!!!!!!!!!! "
                                          + ((string)skl.TranslatedInputs[j]) + @"!!!!!!!!!!!!!!!!!!!! " + pl.Inputs.Count);
                    UnityEngine.Debug.Log (@"!!!! " + inpts.Count + @" COUNT OF INPUT LIST !!!!");
                    if (skl.TranslatedInputs [j].Equals ("A") && !inpts.Contains ("A")) {
                        sk = null;
                    }
                    if (skl.TranslatedInputs [j].Equals ("B") && !inpts.Contains ("B")) {
                        sk = null;
                    }
                    if (skl.TranslatedInputs[j].Equals("C") && !inpts.Contains("C"))
                    {
                        sk = null;
                    }
                    if (skl.TranslatedInputs[j].Equals("D") && !inpts.Contains("D"))
                    {
                        sk = null;
                    }
                    if (skl.TranslatedInputs[j].Equals("Y") && !inpts.Contains("Y"))
                    {
                        sk = null;
                    }
                    if (skl.TranslatedInputs[j].Equals("Z") && !inpts.Contains("Z"))
                    {
                        sk = null;
                    }
                    if (skl.TranslatedInputs [j].Equals ("AB") && !(inpts.Contains ("A") || inpts.Contains ("B")))
                    { 
                        sk = null;
                    }
                    if (skl.TranslatedInputs [j].Equals ("AC") && !(inpts.Contains ("A") || inpts.Contains ("C")))
                    {
                        sk = null;
                    }
                    if (skl.TranslatedInputs[j].Equals("AD") && !(inpts.Contains("A") || inpts.Contains("D")))
                    {
                        sk = null;
                    }
                    if (skl.TranslatedInputs[j].Equals("BC") && !(inpts.Contains("B") || inpts.Contains("C")))
                    {
                        sk = null;
                    }
                    if (skl.TranslatedInputs[j].Equals("BD") && !(inpts.Contains("B") || inpts.Contains("D")))
                    {
                        sk = null;
                    }
                    if (skl.TranslatedInputs[j].Equals("CD") && !(inpts.Contains("C") || inpts.Contains("D")))
                    {
                        sk = null;
                    }
                    if (skl.TranslatedInputs[j].Equals("Down") && !inpts.Contains("Down"))
                    {
                        sk = null;
                    }
                    if (skl.TranslatedInputs[j].Equals ("Forward") && !inpts.Contains ("Forward"))
                    {
                        sk = null;
                    }
                    if (skl.TranslatedInputs[j].Equals ("Up") && !inpts.Contains ("Up"))
                    {
                        sk = null;
                    }
                    if (skl.TranslatedInputs[j].Equals("AIRBORNE") && !pl.IsAirborne)
                    {
                        sk = null;
                    }
                    if (skl.TranslatedInputs[j].Equals("CROUCHING") && !pl.IsCrouching)
                    {
                        sk = null;
                    }
                    if (skl.TranslatedInputs[j].Equals ("RISING") && !pl.MyReel.Name.Contains ("Standing"))
                    {
                        sk = null;
                    }
                    if (((string)skl.TranslatedInputs[j]).StartsWith (@"FRAME>")) {
                        int frameWindowMin = NumberConverter.ConvertToInt (((string)skl.TranslatedInputs[j]).Substring (6, 3));
                        int frameWindowMax = NumberConverter.ConvertToInt (((string)skl.TranslatedInputs[j]).Substring (10, 3));
                        int indx = NumberConverter.ConvertToInt 
                                                  (((string)skl.TranslatedInputs[j]).Substring (((string)skl.TranslatedInputs[j]).LastIndexOf ('e') + 1)) + 1;
                        UnityEngine.Debug.Log (string.Format (@"{0} < {1} < {2}", frameWindowMin, indx, frameWindowMax));
                        if (indx < frameWindowMin || indx > frameWindowMax) {
                            sk = null;
                        }
                    }

                    if (skl.VersusConditionsMet) {
                        sk = null;

                    }
                } else {
                    UnityEngine.Debug.Log("NO INPUTS NIGGA " + pl.Index);

                }
            }
            if (sk != null) {
                UnityEngine.Debug.Log(sk.Name + " IS CHOSEN!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                break;
            }
        }
        return sk;
    }
}

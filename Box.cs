using System;
using UnityEngine;
using System.Collections;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.IO;
using UnityEngine.UI;

public class Box
{
    Locatable owner;
    string name;
    private Boolean visible;
    private Boolean enabled;
    private Texture2D texture;
    string type;
    private string anchorType;
    private string collisionType;
    private RawImage master;
    private RawImage image;
    private Text info;
    private Reel reel;
    private Color hollowColor;
    Vector3 location;


    public Box ()
    {
    }

    public Box (string nm, Texture2D text, string anch, string coll, Reel r)
    {
        reel = r;
        name = nm;
        texture = text;
        anchorType = anch;
        collisionType = coll;
        hollowColor = new Color (Color.white.r, Color.white.g, Color.white.b, Color.white.a / 2);
    }

    public Vector3 MyLocation
    {
        get {
            return location;
        }
        set {
            location = value;
        }
    }

    public Locatable Owner
    {
        get {
            return owner;
        }
        set {
            owner = value;
        }
    }

    public RawImage Image
    {
        get {
            return image;
        }
        set {
            image = value;
        }
    }

    public void setBox (Player p, RawImage img)
    {
        if (Enabled)
        {
            img.texture = texture;
            img.transform.SetAsLastSibling ();
            reel.adjustPlayerSpriteBasedOnScale (p, img);
            if (anchorType == @"Neutral")
            {
                img.rectTransform.pivot = master.rectTransform.pivot;
            }
            img.transform.position = master.transform.position;
            location = img.transform.position;

            if (Visible) {
                UnityEngine.Debug.Log(texture.name + " VISIBLE -------------");
                img.color = hollowColor;
            } else {
                UnityEngine.Debug.Log(texture.name + " INVISIBLE -------------");
                img.color = Color.clear;
            }
        }
    }

    public Boolean collidesWith (Box bx) {
        return location != null && MyLocation.Equals (bx.MyLocation);
    }

    public void checkBox (ArrayList others)
    {
        
    }

    public RawImage Master
    {
        get
        {
            return master;
        }
        set {
            master = value;
        }
    }

    public string Name
    {
        get {
            return name;
        }
        set {
            name = value;
        }
    }

    public Texture2D Texture
    {
        get {
            return texture;
        }
        set
        {
            texture = value;
        }
    }

    public string AnchorType
    {
        get {
            return anchorType;
        }
        set {
            anchorType = value;
        }
    }

    public string CollisionType
    {
        get {
            return collisionType;
        }
        set {
            collisionType = value;
        }
    }

    public Boolean Visible
    {
        get {
            return visible;
        }
        set {
            visible = value;
        }
    }

    public Boolean Enabled
    {
        get {
            return enabled;
        }
        set {
            enabled = value;
        }
    }
}


using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using UnityEngine;

public class DataReader
{
	//private StreamReader streamer;

	public DataReader ()
	{
	}

	public void addAllDialogues ()
	{

	}

	public string concat (string text)
	{
		string finalTxt = @"";

		if ((text.Equals (@"...") && text.Length <= 4) || text.Equals (@"…") && text.Length <= 2) {
			return "grunt";
		}
		for (int i = 0; i < text.Length; i++) {
			if ((text.ToLower () [i] >= 'a' && text.ToLower () [i] <= 'z')) {
				finalTxt += text.ToLower () [i];
			}
		}
		return finalTxt;
	}

	public void addDialogue (string st, ArrayList lst, StreamWriter wrt, StreamWriter main, StreamWriter soundLog, string type) {
		if (!lst.Contains (st) && !st.Equals (@"")) {
			soundLog.WriteLine (concat (st));
			wrt.WriteLine (st + @"   [" + type + @"]");
			lst.Add (st);

			if (main != null) {
				main.WriteLine (st + @"   [" + type + @"]");
			}
		}
	}

	public void addDialogue (string st, StreamWriter wrt, StreamWriter main, StreamWriter soundLog) {

		if (soundLog != null) {
			soundLog.WriteLine (concat (st));
		}

		wrt.WriteLine (@"" + '\n' + st);

		if (main != null) {
			main.WriteLine (@"" + '\n' + st);
		}
	}

	public void addDialogue (Skill s, ArrayList lst, StreamWriter wrt, StreamWriter main, StreamWriter soundLog) {
		for (int j = 0; j < s.Phrases.Length; j++) {
			if (s.MasterSkill == null) {
				addDialogue (s.Phrases [j], lst, wrt, main, soundLog, s.Name);
			} else {
				addDialogue (s.Phrases [j], lst, wrt, main, soundLog, s.MasterSkill.Name + @" --> " + s.Name);
			}
		}
		for (int j = 0; j < s.SpecificPhrases.Length; j++) {
			addDialogue (s.SpecificPhrases [j] [1], lst, wrt, main, soundLog, @"Skill " + s.Name + @" on " + s.SpecificPhrases [j] [0].ToUpperInvariant ());
		}

		if (s.CounterSkill != null) {
			addDialogue (s.CounterSkill, lst, wrt, main, soundLog);
		}

		if (s.TrapSkill != null) {
			addDialogue (s.TrapSkill, lst, wrt, main, soundLog);
		}

		for (int j = 0; j < s.LinkSkills.Count; j++) {
			addDialogue ((Skill)s.LinkSkills [j], lst, wrt, main, soundLog);
		}

	}

	public void saveDialogue (Player p)
	{
		ArrayList phrases = new ArrayList ();
		StreamWriter writer = new StreamWriter (@"Assets/Resources/Players/" + p.SearchName + @"/Info/Speech.txt");
		StreamReader reader = null;
		StreamWriter soundDoc = new StreamWriter (@"Assets/Resources/Players/" + p.SearchName + @"/Sound/Sound.txt");

		StreamReader checkReader = null;

		if (p.Completed) {
			checkReader = new StreamReader (@"Assets/Resources/Data/Dialogue/Dialogue.txt");
		} else {
			checkReader = new StreamReader (@"Assets/Resources/Data/Dialogue/BonusDialogue.txt");
		}

		StreamWriter mainWriter = null;

		Boolean canBeAdded = true;

		while (checkReader.Peek () > 0) {
			if (checkReader.ReadLine ().Contains (p.Name.ToUpper () + @" DIALOGUES")) {
				canBeAdded = false;
			}
		}

		checkReader.Close ();

		if (p.Completed) {
			reader = new StreamReader (@"Assets/Resources/Data/Dialogue/SpecialIntros.txt");
		} else {
			reader = new StreamReader (@"Assets/Resources/Data/Dialogue/BonusSpecialIntros.txt");
		}

		if (canBeAdded) {
			if (p.Completed) {
				mainWriter = new StreamWriter (@"Assets/Resources/Data/Dialogue/Dialogue.txt", true);
			} else {
				mainWriter = new StreamWriter (@"Assets/Resources/Data/Dialogue/BonusDialogue.txt", true);
			}
		}

		string nextLine;

		writer.Write (p.Name.ToUpper () + @" DIALOGUES");
		if (canBeAdded) {
			mainWriter.Write (p.Name.ToUpper () + @" DIALOGUES");
		}
		if (p.Completed) {
			writer.WriteLine (@"");
			if (canBeAdded) {
				mainWriter.WriteLine (@" (COMPLETE)");
			}
		} else {
			writer.WriteLine (@"");
			if (canBeAdded) {
				mainWriter.WriteLine (@"");
			}

		}

		string nm = @"";

		//BASIC
		addDialogue (@"BASIC", writer, mainWriter, soundDoc);

		addDialogue (@"*Light Damage Sound*", phrases, writer, mainWriter, soundDoc, @"In Match");

		addDialogue (@"*Heavy Damage Sound*", phrases, writer, mainWriter, soundDoc, @"In Match");

		addDialogue (@"*Light Attack Sound*", phrases, writer, mainWriter, soundDoc, @"In Match");

		addDialogue (@"*Heavy Attack Sound*", phrases, writer, mainWriter, soundDoc, @"In Match");

		addDialogue (@"*Dizzy Groan*", phrases, writer, mainWriter, soundDoc, @"In Match");

		addDialogue (@"*Generic Grunt*", phrases, writer, mainWriter, soundDoc, @"In Match");

		addDialogue (@"SPECIAL INTRO", writer, mainWriter, soundDoc);
		int numIntros = 0;
		while (reader.Peek () > 0) {
			nextLine = reader.ReadLine ();
			if (nextLine.Contains (p.SearchName)) {

				if (nextLine.Contains (@" ")) {
					string nm1 = nextLine.Substring (0, nextLine.IndexOf (@" ")),
					nm2 = nextLine.Substring (nextLine.IndexOf (@" ") + 1);

					if (!nextLine.Contains (@":")) {

						writer.WriteLine (@"---");
						if (mainWriter != null) {
							mainWriter.WriteLine (@"---");
						}

						if (nm1.Equals (p.SearchName)) {
							nm = nm2;
						} else {
							nm = nm1;
						}
					}
				}
			}
			if (nextLine.StartsWith (p.SearchName + @":")) {
				numIntros++;
				addDialogue (nextLine.Substring (nextLine.IndexOf (@":") + 1), phrases, writer, mainWriter, soundDoc, @"Speaking to " + nm.ToUpperInvariant ());
			}
		}
		if (numIntros == 0) {
			writer.WriteLine (@"N/A");
			if (mainWriter != null) {
				mainWriter.WriteLine (@"N/A");
			}
		}

		reader.Close ();
		//INTRO
		addDialogue (@"INTRO", writer, mainWriter, soundDoc);

		for (int i = 0; i < p.Vocals.Intros.Length; i++) {
			addDialogue (p.Vocals.Intros [i], phrases, writer, mainWriter, soundDoc, @"Intro");
		}
		for (int i = 0; i < p.Vocals.SpecificIntros.Length; i++) {
			addDialogue (p.Vocals.SpecificIntros [i] [1], phrases, writer, mainWriter, soundDoc, @"Intro " + p.Vocals.SpecificIntros [i] [0].ToUpperInvariant ());
		}


		//TAUNT
		addDialogue (@"TAUNT", writer, mainWriter, soundDoc);
		for (int i = 0; i < p.Vocals.Taunts.Length; i++) {
			addDialogue (p.Vocals.Taunts [i], phrases, writer, mainWriter, soundDoc, @"Taunt");
		}

		//VICTORY
		addDialogue (@"VICTORY", writer, mainWriter, soundDoc);
		for (int i = 0; i < p.Vocals.Victories.Length; i++) {
			addDialogue (p.Vocals.Victories [i], phrases, writer, mainWriter, soundDoc, @"Victory");
		}
		for (int i = 0; i < p.Vocals.SpecificVictories.Length; i++) {
			addDialogue (p.Vocals.SpecificVictories [i] [1], phrases, writer, mainWriter, soundDoc, @"Victory " + p.Vocals.SpecificVictories [i] [0].ToUpperInvariant ());
		}

		//CRITICAL
		addDialogue (@"CRITICAL", writer, mainWriter, soundDoc);
		for (int i = 0; i < p.Vocals.Criticals.Length; i++) {
			addDialogue (p.Vocals.Criticals [i], phrases, writer, mainWriter, soundDoc, @"Critical");
		}

		//DEFEAT
		addDialogue (@"DEFEAT", writer, mainWriter, soundDoc);
		for (int i = 0; i < p.Vocals.Defeats.Length; i++) {
			addDialogue (p.Vocals.Defeats [i], phrases, writer, mainWriter, soundDoc, @"Defeated");
		}

		//MATCH VICTORY
		addDialogue (@"MATCH VICTORY", writer, mainWriter, soundDoc);
		for (int i = 0; i < p.Vocals.FinalVictories.Length; i++) {
			addDialogue (p.Vocals.FinalVictories [i], phrases, writer, mainWriter, soundDoc, @"Match Victory");
		}
		for (int i = 0; i < p.Vocals.SpecificFinalVictories.Length; i++) {
			addDialogue (p.Vocals.SpecificFinalVictories [i] [1], phrases, writer, mainWriter, soundDoc, @"Match Victory " + p.Vocals.SpecificFinalVictories [i] [0].ToUpperInvariant ());
		}

		//MATCH DEFEAT
		addDialogue (@"MATCH DEFEAT", writer, mainWriter, soundDoc);
		for (int i = 0; i < p.Vocals.FinalDefeats.Length; i++) {
			addDialogue (p.Vocals.FinalDefeats [i], phrases, writer, mainWriter, soundDoc, @"Match Defeat/Time Over");
		}
		for (int i = 0; i < p.Vocals.SpecificFinalDefeats.Length; i++) {
			addDialogue (p.Vocals.SpecificFinalDefeats [i] [1], phrases, writer, mainWriter, soundDoc, @"Match Defeat/Time Over " + p.Vocals.SpecificFinalDefeats [i] [0].ToUpperInvariant ());
		}

		//ALLY
		addDialogue (@"ASSISTANCE", writer, mainWriter, soundDoc);
		for (int i = 0; i < p.Vocals.Appreciations.Length; i++) {
			addDialogue (p.Vocals.Appreciations [i], phrases, writer, mainWriter, soundDoc, @"Ally");
		}
		for (int i = 0; i < p.Vocals.SpecificAppreciations.Length; i++) {
			addDialogue (p.Vocals.SpecificAppreciations [i] [1], phrases, writer, mainWriter, soundDoc, @"Ally " + p.Vocals.SpecificAppreciations [i] [0].ToUpperInvariant ());
		}

		Skill s;

		addDialogue (@"SKILL", writer, mainWriter, soundDoc);

		for (int i = 0; i < p.AllSkills.Count; i++) {
			s = (Skill)p.AllSkills [i];

			addDialogue (s, phrases, writer, mainWriter, soundDoc);

		}

		s = loadSkill (p, p.SearchName, @"LastStand", @"Player", null);
		addDialogue (s.Phrases [0], phrases, writer, mainWriter, soundDoc, @"Limit Break - " + s.Name);

		writer.WriteLine (@"" + '\n' + '\n' + @"---" + '\n' + '\n');
		if (canBeAdded) {
			mainWriter.WriteLine (@"" + '\n' + '\n' + @"---" + '\n' + '\n');
		}

		writer.Close ();
		soundDoc.Close ();
		if (canBeAdded) {
			mainWriter.Close ();
		}
	}

	public void saveTeam (string fileName, Team team)
	{
		StreamWriter writer = new StreamWriter (@"Assets/Resources/Data/Current/" + fileName + @".txt");
		writer.WriteLine (team.Name);
		writer.WriteLine (team.Abbreviation);
		writer.WriteLine (team.MAXSIZE);
		writer.WriteLine (team.Count);
		for (int i = 0; i < team.Roster.Count; i++) {
			writer.WriteLine (team[i].SearchName + @":" + team[i].PlayStyle + team [i].Ratio);
		}
		writer.Close ();
	}

    /**
	public Team loadTeam (string fileName, Boolean multiplayer)
	{

		//PLAYERS
		StreamReader playerReader = new StreamReader (@"Assets/Resources/Players/Players.txt");

		int pLength = NumberConverter.ConvertToInt (playerReader.ReadLine ().Substring (0, 3));

		ArrayList pNames = new ArrayList ();
		for (int i = 0; i < pLength; i++) {
			pNames.Add (playerReader.ReadLine());
		}
		playerReader.BaseStream.Position = 0;
		playerReader.DiscardBufferedData ();
		playerReader.Close ();

		StreamReader bonusPlayerReader = new StreamReader (@"Assets/Resources/Players/BonusPlayers.txt");

		pLength = NumberConverter.ConvertToInt (bonusPlayerReader.ReadLine ().Substring (0, 3));
		if (pLength > 0) {
			for (int i = 0; i < pLength; i++) {
				pNames.Add (bonusPlayerReader.ReadLine ());
			}
		}
		bonusPlayerReader.BaseStream.Position = 0;
		bonusPlayerReader.DiscardBufferedData ();
		bonusPlayerReader.Close ();

		//BOSSES
		StreamReader bossReader = new StreamReader (@"Assets/Resources/Players/Bosses.txt");

		pLength = NumberConverter.ConvertToInt (bossReader.ReadLine ().Substring (0, 3));

		ArrayList bNames = new ArrayList ();

		for (int i = 0; i < pLength; i++) {
			bNames.Add (bossReader.ReadLine ());
		}
		bossReader.BaseStream.Position = 0;
		bossReader.DiscardBufferedData ();
		bossReader.Close ();

		StreamReader bonusBossReader = new StreamReader (@"Assets/Resources/Players/BonusBosses.txt");

		pLength = NumberConverter.ConvertToInt (bonusBossReader.ReadLine ().Substring (0, 3));

		if (pLength > 0) {
			for (int i = 0; i < pLength; i++) {
				bNames.Add (bonusBossReader.ReadLine ());
			}
		}
		bonusBossReader.BaseStream.Position = 0;
		bonusBossReader.DiscardBufferedData ();
		bonusBossReader.Close ();
			
		StreamReader reader = new StreamReader (@"Assets/Resources/Data/Current/" + fileName + @".txt");

		string name = reader.ReadLine ();
		string abbreviation = reader.ReadLine ();
		int maxSize = NumberConverter.ConvertToInt (reader.ReadLine());
		int count = NumberConverter.ConvertToInt (reader.ReadLine());
		string nextLine = @"", nm = @"";
		char style;
		string ratio;
		Player p;

		Team team = new Team (name, abbreviation, maxSize);

		ArrayList roster = new ArrayList ();

		for (int i = 0; i < count; i++) {

			p = null;

			nextLine = reader.ReadLine ();
			nm = nextLine.Substring (0, nextLine.IndexOf(':'));
			style = nextLine[nextLine.Length - 2];
			ratio = @"" + nextLine [nextLine.Length - 1];
			int index = -1;

			if (!nextLine.Contains (@"random")) {
				p = loadPlayer (nm, @"Player", true, true);
				index = p.Index;
			}

			if (nextLine.Contains (@"randomplayer")) {
				while (team.Contains (index) || !team.canAddClass (p)) {

					nextLine = (string) pNames[new System.Random().Next(pNames.Count)] + @":" + style;
					nm = nextLine.Substring (0, nextLine.IndexOf(':'));
					p = loadPlayer (nm, @"Player", true, true);
					index = p.Index;
				}
			} else if (nextLine.Contains (@"randomboss")) {
				while (team.Contains (index)  || !team.canAddClass (p)) {

					nextLine = (string) bNames[new System.Random().Next(bNames.Count)] + @":" + style;
					nm = nextLine.Substring (0, nextLine.IndexOf(':'));
					p = loadPlayer (nm, @"Player", true, true);
					index = p.Index;
				}
			}

			///p.LoadSprites ();
				
			if (style == 'R') {
				int rnd = new System.Random ().Next (3);
				if (rnd == 0) {
					p.PlayStyle = @"A";
				} else if (rnd == 1) {
					p.PlayStyle = @"B";
				} else {
					p.PlayStyle = @"C";
				}
			} else {
				p.PlayStyle = style + @"";
			}
			if (ratio.Equals (@"1")) {
				p.Ratio = 1;
			}
			if (ratio.Equals (@"2")) {
				p.Ratio = 2;
			}
			if (ratio.Equals (@"3")) {
				p.Ratio = 3;
			}
			if (ratio.Equals (@"4")) {
				p.Ratio = 4;
			}

			p.AdjustForRatio (false);
			team.addPlayer (p);
		}

		if (multiplayer) {
			loadInventory (team);
		}


		string tOutput = @"";
		Player pl;
		for (int i = 0; i < team.Roster.Count; i++) {
			pl = (Player)team.Roster [i];
			tOutput += string.Format (pl.SearchName + pl.PlayStyleDisplay [0]);
			if (i + 1 < team.Roster.Count) {
				tOutput += @"_";
			}
		}

		team.SubTeam = new Team (team.Name, team.Abbreviation, team.MAXSIZE);
		team.BasicTeamOutput = tOutput;

		reader.BaseStream.Position = 0;
		reader.DiscardBufferedData ();
		reader.Close ();

		return team;
	}
	*/

	public void saveMapForBattle (string fileName, string musicInfo) {
		StreamWriter writer = new StreamWriter (@"Assets/Resources/Data/Current/Map.txt");
		writer.WriteLine (fileName);
		writer.WriteLine (musicInfo);
		writer.Close ();
	}

	public Map loadMapForBattle () {
        Player p;
        StreamReader reader = new StreamReader (@"Assets/Resources/Data/Current/Map.txt");
		string name = reader.ReadLine ();
		string music = reader.ReadLine ();

        StreamReader rdr = new StreamReader(@"Assets/Resources/Data/Current/Team1.txt");
        rdr.ReadLine();
        rdr.ReadLine();
        rdr.ReadLine();
        rdr.ReadLine();
        string str = rdr.ReadLine(); 
        p = loadRandomPlayer (str.Substring(0, str.IndexOf (':')), @"Player", @"", true);
        rdr.Close();

		if (name.StartsWith (@"random")) {
			StreamReader mapReader = new StreamReader (@"Assets/Resources/Maps/Maps.txt");
			string[] mapNames = new string[NumberConverter.ConvertToInt (mapReader.ReadLine ().Substring (0, 3))];
			for (int i = 0; i < mapNames.Length; i++) {
				mapNames [i] = mapReader.ReadLine ();
			}
			name = mapNames [new System.Random ().Next (mapNames.Length)];

			mapReader.BaseStream.Position = 0;
			mapReader.DiscardBufferedData ();
			mapReader.Close ();
		} else if (name.StartsWith (@"player")) {
			name = p.StageName;
		}

		if (music.StartsWith (@"player")) {
			music = p.SearchName;
		} else if (music.StartsWith (@"random")) {
			music = @"random";
		} else {
			music = @"stage";
		}

		reader.BaseStream.Position = 0;
		reader.DiscardBufferedData ();
		reader.Close ();
		return loadMap (name, music);
	}

	public Player[] loadPlayers ()
	{

		StreamReader streamer = new StreamReader (@"Assets/Resources/Players/Players.txt");
		StreamReader bonus = new StreamReader (@"Assets/Resources/Players/BonusPlayers.txt");
		streamer.DiscardBufferedData ();
		bonus.DiscardBufferedData ();

		int length1 = NumberConverter.ConvertToInt(streamer.ReadLine ().Substring (0, 3)),
		length2 = NumberConverter.ConvertToInt(bonus.ReadLine ().Substring (0, 3));

		Player [] list = new Player [length1 + length2];
		int index = 0;
		while (index < list.Length) {
			for (int j = 0; j < length1; j++) {
				list [index] = loadPlayer (streamer.ReadLine (), @"Player", false, true, false);
				index++;
			}
			for (int j = 0; j < length2; j++) {
                list [index] = loadPlayer (bonus.ReadLine (), @"Player", false, true, false);
				index++;
			}
		}

		bonus.BaseStream.Position = 0;
		bonus.DiscardBufferedData ();
		streamer.BaseStream.Position = 0;
		streamer.DiscardBufferedData ();
		bonus.Close ();
		streamer.Close ();
		return list;
	}

	public Map[] loadMaps ()
	{
		StreamReader streamer = new StreamReader (@"Assets/Resources/Maps/Maps.txt");
		Map[] list = new Map[NumberConverter.ConvertToInt(streamer.ReadLine().Substring(0, 3))];
		for (int i = 0; i < list.Length; i++) {
			list [i] = loadMap (streamer.ReadLine(), @"");
		}
		streamer.BaseStream.Position = 0;
		streamer.DiscardBufferedData ();
		streamer.Close ();
		return list;
	}

	public Player[] loadBosses ()
	{
		StreamReader streamer = new StreamReader (@"Assets/Resources/Players/Bosses.txt");
		StreamReader bonus = new StreamReader (@"Assets/Resources/Players/BonusBosses.txt");

		int length1 = NumberConverter.ConvertToInt(streamer.ReadLine().Substring(0, 3)),
		length2 = NumberConverter.ConvertToInt(bonus.ReadLine().Substring(0, 3));

		Player[] list = new Player[length1 + length2];
		int index = 0;
		while (index < list.Length) {
			for (int j = 0; j < length1; j++) {
				list [index] = loadPlayer (streamer.ReadLine (), @"Player", false, true, false);
				index++;
			}
			for (int j = 0; j < length2; j++) {
				list [index] = loadPlayer (bonus.ReadLine (), @"Player", false, true, false);
				index++;
			}
		}

		streamer.BaseStream.Position = 0;
		streamer.DiscardBufferedData ();
		bonus.BaseStream.Position = 0;
		bonus.DiscardBufferedData ();
		streamer.Close ();
		bonus.Close ();
		return list;
	}

    public void loadTeam (Map mp)
    {
        //TEAM1
        StreamReader teamReader = new StreamReader (@"Assets/Resources/Data/Current/Team1.txt");
        mp.Team1 = new Team (@"Team 1", @"1", 4);
        mp.Team1.Name = teamReader.ReadLine ();
        mp.Team1.Abbreviation = teamReader.ReadLine ();
        mp.Team1.MAXSIZE = NumberConverter.ConvertToInt(teamReader.ReadLine());
        int size = NumberConverter.ConvertToInt (teamReader.ReadLine());

        int ratio;
        string name, type;

        for (int i = 0; i < size; i ++) {
            name = teamReader.ReadLine();
            type = name.Substring (name.IndexOf(':') + 1, 1);
            ratio = NumberConverter.ConvertToInt (name.Substring(name.IndexOf (':') + 2, 1));
            name = name.Substring(0, name.IndexOf(':'));
            mp.Team1.addPlayer (loadTeamMember (mp.Team1, name, type, ratio, true));
        }
        teamReader.Close ();
        loadInventory(mp.Team1);

        //TEAM2
        teamReader = new StreamReader(@"Assets/Resources/Data/Current/Team2.txt");
        mp.Team2 = new Team(@"Team 2", @"2", 4);
        mp.Team2.Name = teamReader.ReadLine ();
        mp.Team2.Abbreviation = teamReader.ReadLine ();
        mp.Team2.MAXSIZE = NumberConverter.ConvertToInt (teamReader.ReadLine ());
        size = NumberConverter.ConvertToInt(teamReader.ReadLine ());

        for (int i = 0; i < size; i++)
        {
            name = teamReader.ReadLine ();
            type = name.Substring(name.IndexOf (':') + 1, 1);
            ratio = NumberConverter.ConvertToInt(name.Substring(name.IndexOf (':') + 2, 1));
            name = name.Substring (0, name.IndexOf (':'));
            mp.Team2.addPlayer (loadTeamMember (mp.Team2, name, type, ratio, true));
        }
        teamReader.Close ();
        loadInventory(mp.Team2);

    }

    public Player loadTeamMember (Team tm, string name, string type, int ratio, Boolean tactical)
    {
        StreamReader roster = new StreamReader (@"Assets/Resources/Players/Players.txt");
        string[] names, 
            namesPl = new string[NumberConverter.ConvertToInt (roster.ReadLine ().Substring (0, 3))],
            namesBoss;
        ///string nm;
        for (int i = 0; i < namesPl.Length; i++)
        {
            namesPl[i] = roster.ReadLine ();
        }
        roster.Close ();

        roster = new StreamReader (@"Assets/Resources/Players/Bosses.txt");
        namesBoss = new string[NumberConverter.ConvertToInt (roster.ReadLine().Substring(0, 3))];
        for (int i = 0; i < namesBoss.Length; i ++)
        {
            namesBoss [i] = roster.ReadLine ();
        }
        roster.Close();

        names = new string [namesPl.Length + namesBoss.Length];
        for (int i = 0; i < namesPl.Length; i++)
        {
           names [i] = namesPl [i];
        }
        for (int i = 0 + namesPl.Length; i < namesBoss.Length; i++)
        {
            names [i] = namesBoss[i];
        }        

        if (name == @"randomplayer")
        {
            name = namesPl [new System.Random ().Next (namesPl.Length)];
            while (tm.Contains (name))
            {
                name = names [new System.Random ().Next (namesPl.Length)];
            }
        }

        if (name == @"randomboss")
        {
            name = namesBoss[new System.Random().Next(namesBoss.Length)];
        }

        if (name == @"nm")
        {
            name = names [new System.Random().Next(names.Length)];
        }

        Player player = loadPlayer (name, @"Player", true, true, tactical);

        if (type == "R")
        {
            int rnd = new System.Random ().Next (3);
            if (rnd == 0)
            {
                player.PlayStyle = @"A";
            }
            else if (rnd == 1)
            {
                player.PlayStyle = @"B";
            }
            else
            {
                player.PlayStyle = @"C";
            }
        }
        else
        {
            player.PlayStyle = type + @"";
        }

        player.Ratio = ratio;

        player.AdjustForRatio(false);

        return player;
    }

    public Player loadPlayerForVersus ()
    {
        Player player = new Player (-1, @"", @"", @"", @"", @"", @"", @"", @"", 0, 0.0, 0.0, @"A", @"A", 1, -1, false, false);

        return player;
    }

	public Player loadPlayer (string searchName, string type, Boolean loadSkill, Boolean loadSprites, Boolean tactical)
	{
		StreamReader streamer = null;

		if (type.Equals (@"Player")) {
			streamer = new StreamReader (@"Assets/Resources/Players/" + searchName + @"/Stats.txt");
		} else if (type.Equals (@"Map")) {
			streamer = new StreamReader (@"Assets/Resources/Maps/" + searchName + @"/" + searchName + @"/Stats.txt");
		}
		//int index = NumberConverter.ConvertToInt (streamer.ReadLine());
		int index = NumberConverter.ConvertToInt(streamer.ReadLine());
		string firstName = streamer.ReadLine ();
		string surname = streamer.ReadLine ();

		string stageName = streamer.ReadLine ();
		string sex = streamer.ReadLine ();
		string sexualPreferences = streamer.ReadLine ();
		string species = streamer.ReadLine ();
		string nationality = streamer.ReadLine ();
		int age = NumberConverter.ConvertToInt (streamer.ReadLine());
		double weight = Convert.ToDouble (streamer.ReadLine());
		double height = Convert.ToDouble (streamer.ReadLine());
		string playerClass = streamer.ReadLine ();
		string style = streamer.ReadLine ();
		int ratio = NumberConverter.ConvertToInt (streamer.ReadLine());

		streamer.ReadLine ();

		string health = streamer.ReadLine ();
		int hp = NumberConverter.ConvertToInt(health.Substring (0, 5)), hpRegen = NumberConverter.ConvertToInt(health.Substring (6, 5));

		string rush = streamer.ReadLine ();
		int rm = NumberConverter.ConvertToInt(rush.Substring (0, 5)), rmRegen = NumberConverter.ConvertToInt(rush.Substring (6, 5));

		string guard = streamer.ReadLine ();
		int gm = NumberConverter.ConvertToInt(guard.Substring (0, 5)), gmRegen = NumberConverter.ConvertToInt(guard.Substring (6, 5));

		string vitality = streamer.ReadLine ();
		int vm = NumberConverter.ConvertToInt(vitality.Substring (0, 5)), vmRegen = NumberConverter.ConvertToInt(vitality.Substring (6, 5));

		streamer.ReadLine ();

		int strength = NumberConverter.ConvertToInt(streamer.ReadLine ().Substring(0, 5));
		int grit = NumberConverter.ConvertToInt(streamer.ReadLine ().Substring(0, 5));
		int magick = NumberConverter.ConvertToInt(streamer.ReadLine ().Substring(0, 5));
		int resistance = NumberConverter.ConvertToInt(streamer.ReadLine ().Substring(0, 5));
		double dexterity = Convert.ToDouble(streamer.ReadLine ().Substring(0, 5));
		double speed = Convert.ToDouble(streamer.ReadLine ().Substring(0, 5));
		double proration = Convert.ToDouble(streamer.ReadLine ().Substring(0, 5));
		int movement = NumberConverter.ConvertToInt(streamer.ReadLine ().Substring(0, 5));
		int teamwork = NumberConverter.ConvertToInt(streamer.ReadLine ().Substring(0, 5));
		int luck = NumberConverter.ConvertToInt(streamer.ReadLine ().Substring(0, 5));
		int actionPoints = NumberConverter.ConvertToInt(streamer.ReadLine ().Substring(0, 5));

		streamer.ReadLine ();

		double stun = Convert.ToDouble (streamer.ReadLine().Substring(0, 6));
		double grounded = Convert.ToDouble (streamer.ReadLine().Substring(0, 6));
		double airborne = Convert.ToDouble (streamer.ReadLine().Substring(0, 6));
		double counter = Convert.ToDouble (streamer.ReadLine().Substring(0, 6));
		double parry = Convert.ToDouble (streamer.ReadLine().Substring(0, 6));
		double poison = Convert.ToDouble (streamer.ReadLine().Substring(0, 6));

		double regen = Convert.ToDouble (streamer.ReadLine().Substring(0, 6));
		double daze = Convert.ToDouble (streamer.ReadLine().Substring(0, 6));
		double confuse = Convert.ToDouble (streamer.ReadLine().Substring(0, 6));
		double sadness = Convert.ToDouble (streamer.ReadLine().Substring(0, 6));
		double fury = Convert.ToDouble (streamer.ReadLine().Substring(0, 6));
		double sleep = Convert.ToDouble (streamer.ReadLine().Substring(0, 6));

		double adle = Convert.ToDouble (streamer.ReadLine().Substring(0, 6));
		double freeze = Convert.ToDouble (streamer.ReadLine().Substring(0, 6));
		double immune = Convert.ToDouble (streamer.ReadLine().Substring(0, 6));
		double invulnerable = Convert.ToDouble (streamer.ReadLine().Substring(0, 6));
		double burn = Convert.ToDouble (streamer.ReadLine().Substring(0, 6));
		double learn = Convert.ToDouble (streamer.ReadLine().Substring(0, 6));
		double invisible = Convert.ToDouble (streamer.ReadLine().Substring(0, 6));
		double blind = Convert.ToDouble (streamer.ReadLine().Substring(0, 6));


		streamer.ReadLine ();

		string fire = streamer.ReadLine ();
		int fireOffense = NumberConverter.ConvertToInt (fire.Substring(0, 3)), fireDefense = NumberConverter.ConvertToInt(fire.Substring(4, 3));

		string ice = streamer.ReadLine ();
		int iceOffense = NumberConverter.ConvertToInt (ice.Substring(0, 3)), iceDefense = NumberConverter.ConvertToInt (ice.Substring(4, 3));

		string electricity = streamer.ReadLine ();
		int electricityOffense = NumberConverter.ConvertToInt (electricity.Substring(0, 3)), electricityDefense = NumberConverter.ConvertToInt (electricity.Substring(4, 3));

		string wind = streamer.ReadLine ();
		int windOffense = NumberConverter.ConvertToInt (wind.Substring(0, 3)), windDefense = NumberConverter.ConvertToInt (wind.Substring(4, 3));

		string water = streamer.ReadLine ();
		int waterOffense = NumberConverter.ConvertToInt (water.Substring(0, 3)), waterDefense = NumberConverter.ConvertToInt (water.Substring(4, 3));

		string earth = streamer.ReadLine ();
		int earthOffense = NumberConverter.ConvertToInt (earth.Substring(0, 3)), earthDefense = NumberConverter.ConvertToInt (earth.Substring(4, 3));

		string metal = streamer.ReadLine ();
		int metalOffense = NumberConverter.ConvertToInt (metal.Substring(0, 3)), metalDefense = NumberConverter.ConvertToInt (metal.Substring(4, 3));

		string darkness = streamer.ReadLine ();
		int darknessOffense = NumberConverter.ConvertToInt (darkness.Substring(0, 3)), darknessDefense = NumberConverter.ConvertToInt (darkness.Substring(4, 3));

		string light = streamer.ReadLine ();
		int lightOffense = NumberConverter.ConvertToInt (light.Substring(0, 3)), lightDefense = NumberConverter.ConvertToInt (light.Substring(4, 3));

		string breaking = streamer.ReadLine ();
		int breakOffense = NumberConverter.ConvertToInt (breaking.Substring(0, 3)), breakDefense = NumberConverter.ConvertToInt (breaking.Substring(4, 3));

		int timeRemaining = NumberConverter.ConvertToInt (streamer.ReadLine().Substring(0, 3));

		string[] specialConditions = new string[NumberConverter.ConvertToInt(streamer.ReadLine ().Substring(0, 3))];
		for (int i = 0; i < specialConditions.Length; i++) {
			specialConditions [i] = streamer.ReadLine ();
		}


		streamer.BaseStream.Position = 0;
		streamer.DiscardBufferedData ();
		streamer.Close ();

		Player newPlayer = new Player (index, firstName, surname, searchName, stageName, sex, sexualPreferences, species,
                                       nationality, age, weight, height, playerClass, style, ratio, timeRemaining, loadSprites, tactical);
		newPlayer.setStates (stun, grounded, airborne, counter, parry, poison, regen, daze, confuse,
			sadness, fury, sleep, adle, freeze, immune, invulnerable, burn, learn, invisible);
		newPlayer.setBaseStats (strength, grit, magick, resistance, dexterity, speed, proration, movement, teamwork, luck, actionPoints);
		newPlayer.setMeters (hp, hpRegen, rm, rmRegen, gm, gmRegen, vm, vmRegen);
		//loadStates (stun, grounded, airborne, counter, parry, poison, regen, daze, confuse,
		//	sadness, fury, sleep, adle, freeze, immune, invulnerable, burn, learn, invisible);
		newPlayer.setElementalProficiency (fireOffense, fireDefense, iceOffense, iceDefense, electricityOffense,
			electricityDefense, windOffense, windDefense, waterOffense, waterDefense,
			earthOffense, earthDefense, metalOffense, metalDefense, darknessOffense,
			darknessDefense, lightOffense, lightDefense, breakOffense, breakDefense);

		newPlayer.Vocals = readVoice (searchName, type);
		newPlayer.setConditions (specialConditions);


		Player target = new Player (-1, @"", @"", @"", @"", @"", @"", @"", @"", 0, 0.0, 0.0, @"A", @"A", 1, -1, false, false);
		target.setStates (0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
		target.setElementalProficiency (0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
		target.setBaseStats (100, 100, 100, 100, 0.9, 0.5, .9, 4, 20, 20, 5);
		target.MyTeam = new Team (@"False Team", @"FT", 4);

		newPlayer.PlayerStandard = target;

		if (type.Equals (@"Player")) {
            if (loadSkill) {
                loadSkills(newPlayer, @"Player");
            }
            loadItems (newPlayer);
			saveDialogue (newPlayer);
		}

		return newPlayer;
	}

	public Player loadRandomPlayer (string searchName, string type, string nm, Boolean tactical)
	{
		StreamReader streamer = null;

		if (searchName.Contains (@"randomplayer")) {
			StreamReader playerReader = new StreamReader (@"Assets/Resources/Players/Players.txt");
			StreamReader bonusPlayerReader = new StreamReader (@"Assets/Resources/Players/BonusPlayers.txt");

			int l1 = NumberConverter.ConvertToInt (playerReader.ReadLine ().Substring (0, 3)),
			l2 = NumberConverter.ConvertToInt (bonusPlayerReader.ReadLine ().Substring (0, 3));

			string[] allPlayers = new string[l1 + l2];

			int i = 0;
			while (i < l1) {
				allPlayers [i] = playerReader.ReadLine ();
				i++;
			}

			while (i < allPlayers.Length) {
				allPlayers [i] = bonusPlayerReader.ReadLine ();
				i++;
			}

			while (searchName.Equals (nm) || searchName.Contains (@"random")) {
				searchName = allPlayers [new System.Random ().Next (allPlayers.Length)];
			}

			playerReader.BaseStream.Position = 0;
			playerReader.DiscardBufferedData ();
			bonusPlayerReader.BaseStream.Position = 0;
			bonusPlayerReader.DiscardBufferedData ();
			playerReader.Close ();
			bonusPlayerReader.Close ();
		}

		else if (searchName.Contains (@"randomboss")) {
			StreamReader playerReader = new StreamReader (@"Assets/Resources/Players/Bosses.txt");
			StreamReader bonusPlayerReader = new StreamReader (@"Assets/Resources/Players/BonusBosses.txt");

			int l1 = NumberConverter.ConvertToInt (playerReader.ReadLine ().Substring (0, 3)),
			l2 = NumberConverter.ConvertToInt (bonusPlayerReader.ReadLine ().Substring (0, 3));

			string[] allPlayers = new string[l1 + l2];

			int i = 0;
			while (i < l1) {
				allPlayers [i] = playerReader.ReadLine ();
				i++;
			}

			while (i < allPlayers.Length) {
				allPlayers [i] = bonusPlayerReader.ReadLine ();
				i++;
			}

			while (searchName.Equals (nm) || searchName.Contains (@"random")) {
				searchName = allPlayers [new System.Random ().Next (allPlayers.Length)];
			}

			playerReader.BaseStream.Position = 0;
			playerReader.DiscardBufferedData ();
			bonusPlayerReader.BaseStream.Position = 0;
			bonusPlayerReader.DiscardBufferedData ();
			playerReader.Close ();
			bonusPlayerReader.Close ();
		}



		if (type.Equals (@"Player")) {
			streamer = new StreamReader (@"Assets/Resources/Players/" + searchName + @"/Stats.txt");
		} else if (type.Equals (@"Map")) {
			streamer = new StreamReader (@"Assets/Resources/Maps/" + searchName + @"/" + searchName + @"/Stats.txt");
		}
		int index = NumberConverter.ConvertToInt(streamer.ReadLine());
		string firstName = streamer.ReadLine ();
		string surname = streamer.ReadLine ();

		string stageName = streamer.ReadLine ();
		string sex = streamer.ReadLine ();
		string sexualPreferences = streamer.ReadLine ();
		string species = streamer.ReadLine ();
		string nationality = streamer.ReadLine ();
		int age = NumberConverter.ConvertToInt (streamer.ReadLine());
		double weight = Convert.ToDouble (streamer.ReadLine());
		double height = Convert.ToDouble (streamer.ReadLine());
		string playerClass = streamer.ReadLine ();
		string style = streamer.ReadLine ();
		int ratio = NumberConverter.ConvertToInt (streamer.ReadLine());

		streamer.ReadLine ();

		string health = streamer.ReadLine ();
		int hp = NumberConverter.ConvertToInt(health.Substring (0, 5)), hpRegen = NumberConverter.ConvertToInt(health.Substring (6, 5));

		string rush = streamer.ReadLine ();
		int rm = NumberConverter.ConvertToInt(rush.Substring (0, 5)), rmRegen = NumberConverter.ConvertToInt(rush.Substring (6, 5));

		string guard = streamer.ReadLine ();
		int gm = NumberConverter.ConvertToInt(guard.Substring (0, 5)), gmRegen = NumberConverter.ConvertToInt(guard.Substring (6, 5));

		string vitality = streamer.ReadLine ();
		int vm = NumberConverter.ConvertToInt(vitality.Substring (0, 5)), vmRegen = NumberConverter.ConvertToInt(vitality.Substring (6, 5));

		streamer.ReadLine ();

		int strength = NumberConverter.ConvertToInt(streamer.ReadLine ().Substring(0, 5));
		int grit = NumberConverter.ConvertToInt(streamer.ReadLine ().Substring(0, 5));
		int magick = NumberConverter.ConvertToInt(streamer.ReadLine ().Substring(0, 5));
		int resistance = NumberConverter.ConvertToInt(streamer.ReadLine ().Substring(0, 5));
		double dexterity = Convert.ToDouble(streamer.ReadLine ().Substring(0, 5));
		double speed = Convert.ToDouble(streamer.ReadLine ().Substring(0, 5));
		double proration = Convert.ToDouble(streamer.ReadLine ().Substring(0, 5));
		int movement = NumberConverter.ConvertToInt(streamer.ReadLine ().Substring(0, 5));
		int teamwork = NumberConverter.ConvertToInt(streamer.ReadLine ().Substring(0, 5));
		int luck = NumberConverter.ConvertToInt(streamer.ReadLine ().Substring(0, 5));
		int actionPoints = NumberConverter.ConvertToInt(streamer.ReadLine ().Substring(0, 5));

		streamer.ReadLine ();

		double stun = Convert.ToDouble (streamer.ReadLine().Substring(0, 6));
		double grounded = Convert.ToDouble (streamer.ReadLine().Substring(0, 6));
		double airborne = Convert.ToDouble (streamer.ReadLine().Substring(0, 6));
		double counter = Convert.ToDouble (streamer.ReadLine().Substring(0, 6));
		double parry = Convert.ToDouble (streamer.ReadLine().Substring(0, 6));
		double poison = Convert.ToDouble (streamer.ReadLine().Substring(0, 6));

		double regen = Convert.ToDouble (streamer.ReadLine().Substring(0, 6));
		double daze = Convert.ToDouble (streamer.ReadLine().Substring(0, 6));
		double confuse = Convert.ToDouble (streamer.ReadLine().Substring(0, 6));
		double sadness = Convert.ToDouble (streamer.ReadLine().Substring(0, 6));
		double fury = Convert.ToDouble (streamer.ReadLine().Substring(0, 6));
		double sleep = Convert.ToDouble (streamer.ReadLine().Substring(0, 6));

		double adle = Convert.ToDouble (streamer.ReadLine().Substring(0, 6));
		double freeze = Convert.ToDouble (streamer.ReadLine().Substring(0, 6));
		double immune = Convert.ToDouble (streamer.ReadLine().Substring(0, 6));
		double invulnerable = Convert.ToDouble (streamer.ReadLine().Substring(0, 6));
		double burn = Convert.ToDouble (streamer.ReadLine().Substring(0, 6));
		double learn = Convert.ToDouble (streamer.ReadLine().Substring(0, 6));
		double invisible = Convert.ToDouble (streamer.ReadLine().Substring(0, 6));
		double blind = Convert.ToDouble (streamer.ReadLine().Substring(0, 6));

		streamer.ReadLine ();

		string fire = streamer.ReadLine ();
		int fireOffense = NumberConverter.ConvertToInt (fire.Substring(0, 3)), fireDefense = NumberConverter.ConvertToInt(fire.Substring(4, 3));

		string ice = streamer.ReadLine ();
		int iceOffense = NumberConverter.ConvertToInt (ice.Substring(0, 3)), iceDefense = NumberConverter.ConvertToInt (ice.Substring(4, 3));

		string electricity = streamer.ReadLine ();
		int electricityOffense = NumberConverter.ConvertToInt (electricity.Substring(0, 3)), electricityDefense = NumberConverter.ConvertToInt (electricity.Substring(4, 3));

		string wind = streamer.ReadLine ();
		int windOffense = NumberConverter.ConvertToInt (wind.Substring(0, 3)), windDefense = NumberConverter.ConvertToInt (wind.Substring(4, 3));

		string water = streamer.ReadLine ();
		int waterOffense = NumberConverter.ConvertToInt (water.Substring(0, 3)), waterDefense = NumberConverter.ConvertToInt (water.Substring(4, 3));

		string earth = streamer.ReadLine ();
		int earthOffense = NumberConverter.ConvertToInt (earth.Substring(0, 3)), earthDefense = NumberConverter.ConvertToInt (earth.Substring(4, 3));

		string metal = streamer.ReadLine ();
		int metalOffense = NumberConverter.ConvertToInt (metal.Substring(0, 3)), metalDefense = NumberConverter.ConvertToInt (metal.Substring(4, 3));

		string darkness = streamer.ReadLine ();
		int darknessOffense = NumberConverter.ConvertToInt (darkness.Substring(0, 3)), darknessDefense = NumberConverter.ConvertToInt (darkness.Substring(4, 3));

		string light = streamer.ReadLine ();
		int lightOffense = NumberConverter.ConvertToInt (light.Substring(0, 3)), lightDefense = NumberConverter.ConvertToInt (light.Substring(4, 3));

		string breaking = streamer.ReadLine ();
		int breakOffense = NumberConverter.ConvertToInt (breaking.Substring(0, 3)), breakDefense = NumberConverter.ConvertToInt (breaking.Substring(4, 3));

		int timeRemaining = NumberConverter.ConvertToInt (streamer.ReadLine().Substring(0, 3));

		string[] specialConditions = new string[NumberConverter.ConvertToInt(streamer.ReadLine ().Substring(0, 3))];
		for (int i = 0; i < specialConditions.Length; i++) {
			specialConditions [i] = streamer.ReadLine ();
		}

		streamer.BaseStream.Position = 0;
		streamer.DiscardBufferedData ();
		streamer.Close ();

		Player newPlayer = new Player (index, firstName, surname, searchName, stageName, sex, sexualPreferences, species, nationality, age, weight, height, playerClass, style, ratio, timeRemaining, true, tactical);
		newPlayer.setBaseStats (strength, grit, magick, resistance, dexterity, speed, proration, movement, teamwork, luck, actionPoints);
		newPlayer.setMeters (hp, hpRegen, rm, rmRegen, gm, gmRegen, vm, vmRegen);
		newPlayer.setStates (stun, grounded, airborne, counter, parry, poison, regen, daze, confuse,
			sadness, fury, sleep, adle, freeze, immune, invulnerable, burn, learn, invisible);
		newPlayer.setElementalProficiency (fireOffense, fireDefense, iceOffense, iceDefense, electricityOffense,
			electricityDefense, windOffense, windDefense, waterOffense, waterDefense,
			earthOffense, earthDefense, metalOffense, metalDefense, darknessOffense,
			darknessDefense, lightOffense, lightDefense, breakOffense, breakDefense);

		newPlayer.Vocals = readVoice (searchName, type);
		newPlayer.setConditions (specialConditions);
		if (type.Equals (@"Player")) {
			loadSkills (newPlayer, @"Player");
			loadItems (newPlayer);
		}

		newPlayer.PlayerStandard = newPlayer;

		return newPlayer;
	}


	public ArrayList loadDialogue (Player p1, Player p2)
	{

		ArrayList dialogues = new ArrayList ();
		StreamReader rdr;
		string line = @"";

		if (p1.Completed && p2.Completed) {
			rdr = new StreamReader (@"Assets/Resources/Data/Dialogue/SpecialIntros.txt");
		} else {
			rdr = new StreamReader (@"Assets/Resources/Data/Dialogue/BonusSpecialIntros.txt");
		}

		while (rdr.Peek () > 0) {
			line = rdr.ReadLine ();
			if (line.Contains (p1.SearchName) && line.Contains (p2.SearchName) && p1.SearchName != p2.SearchName) {
				break;
			}
		}

		if (rdr.Peek () == -1) {
			return dialogues;
		}

		line = rdr.ReadLine ();

		while (!line.Contains (@"-END")) {

			dialogues.Add (line);

			line = rdr.ReadLine ();
		}




			rdr.Close ();
			return dialogues;





	}

	public Voice readVoice (string searchName, string type)
	{
		StreamReader streamer = null;
		if (type.Equals (@"Player")) {
			streamer = new StreamReader (@"Assets/Resources/Players/" + searchName + @"/Voice.txt");
		} else {
			streamer = new StreamReader (@"Assets/Resources/Maps/" + searchName + @"/" + searchName + @"/Voice.txt");
		}

		string[] intros = new string[NumberConverter.ConvertToInt (streamer.ReadLine().Substring(5, 2))];
		for (int i = 0; i < intros.Length; i++) {
			intros [i] = streamer.ReadLine ();
		}

		string[][] specificIntros = new string[NumberConverter.ConvertToInt (streamer.ReadLine().Substring(5, 2))][];
		string specificIntro;
		for (int i = 0; i < specificIntros.Length; i++) {
			specificIntro = streamer.ReadLine ();
			specificIntros[i] = new string[2];
			specificIntros [i] [0] = specificIntro.Substring(0, specificIntro.IndexOf(':'));
			specificIntros [i] [1] = specificIntro.Substring (specificIntro.IndexOf(':') + 1);

		}

		string[] taunts = new string[NumberConverter.ConvertToInt (streamer.ReadLine().Substring(5, 2))];
		for (int i = 0; i < taunts.Length; i++) {
			taunts [i] = streamer.ReadLine ();
		}

		string[] victories = new string[NumberConverter.ConvertToInt (streamer.ReadLine().Substring(5, 2))];
		for (int i = 0; i < victories.Length; i++) {
			victories [i] = streamer.ReadLine ();
		}

		string[][] specificVictories = new string[NumberConverter.ConvertToInt (streamer.ReadLine().Substring(5, 2))][];
		string specificVictory;
		for (int i = 0; i < specificVictories.Length; i++) {
			specificVictory = streamer.ReadLine ();
			specificVictories[i] = new string[2];
			specificVictories [i] [0] = specificVictory.Substring(0, specificVictory.IndexOf(':'));
			specificVictories [i] [1] = specificVictory.Substring (specificVictory.IndexOf(':') + 1);

		}

		string[] criticals = new string[NumberConverter.ConvertToInt (streamer.ReadLine().Substring(5, 2))];
		for (int i = 0; i < criticals.Length; i++) {
			criticals [i] = streamer.ReadLine ();
		}

		string[] defeats = new string[NumberConverter.ConvertToInt (streamer.ReadLine().Substring(5, 2))];
		for (int i = 0; i < defeats.Length; i++) {
			defeats [i] = streamer.ReadLine ();
		}

		string[] finalVictories = new string[NumberConverter.ConvertToInt (streamer.ReadLine().Substring(5, 2))];
		for (int i = 0; i < finalVictories.Length; i++) {
			finalVictories [i] = streamer.ReadLine ();
		}

		string[][] specificFinalVictories = new string[NumberConverter.ConvertToInt (streamer.ReadLine().Substring(5, 2))][];
		string specificFinalVictory;
		for (int i = 0; i < specificFinalVictories.Length; i++) {
			specificFinalVictory = streamer.ReadLine ();
			specificFinalVictories[i] = new string[2];
			specificFinalVictories [i] [0] = specificFinalVictory.Substring(0, specificFinalVictory.IndexOf(':'));
			specificFinalVictories [i] [1] = specificFinalVictory.Substring (specificFinalVictory.IndexOf(':') + 1);
		}

		string[] finalDefeats = new string[NumberConverter.ConvertToInt (streamer.ReadLine().Substring(5, 2))];
		for (int i = 0; i < finalDefeats.Length; i++) {
			finalDefeats [i] = streamer.ReadLine ();
		}

		string[][] specificFinalDefeats = new string[NumberConverter.ConvertToInt (streamer.ReadLine().Substring(5, 2))][];
		string specificFinalDefeat;
		for (int i = 0; i < specificFinalDefeats.Length; i++) {
			specificFinalDefeat = streamer.ReadLine ();
			specificFinalDefeats[i] = new string[2];
			specificFinalDefeats [i] [0] = specificFinalDefeat.Substring(0, specificFinalDefeat.IndexOf(':'));
			specificFinalDefeats [i] [1] = specificFinalDefeat.Substring (specificFinalDefeat.IndexOf(':') + 1);
		}

		string[] appreciations = new string[NumberConverter.ConvertToInt (streamer.ReadLine().Substring(5, 2))];
		for (int i = 0; i < appreciations.Length; i++) {
			appreciations [i] = streamer.ReadLine ();
		}

		string[][] specificAppreciations = new string[NumberConverter.ConvertToInt (streamer.ReadLine().Substring(5, 2))][];
		string specificAppreciation;
		for (int i = 0; i < specificAppreciations.Length; i++) {
			specificAppreciation = streamer.ReadLine ();
			specificAppreciations[i] = new string[2];
			specificAppreciations [i] [0] = specificAppreciation.Substring(0, specificAppreciation.IndexOf(':'));
			specificAppreciations [i] [1] = specificAppreciation.Substring (specificAppreciation.IndexOf(':') + 1);

		}
		streamer.BaseStream.Position = 0;
		streamer.DiscardBufferedData ();
		streamer.Close ();
		return new Voice (intros, specificIntros, taunts, victories, specificVictories, criticals, defeats,
			finalVictories, specificFinalVictories, finalDefeats, specificFinalVictories, appreciations, specificAppreciations);
	}

	public string loadRandomMusic ()
	{
		StreamReader rdr = new StreamReader (@"Assets/Resources/Music/AllMusic.txt");
		ArrayList musicList = new ArrayList ();
		int linesOfInfo = NumberConverter.ConvertToInt (rdr.ReadLine ().Substring (0, 3));
		for (int i = 0; i < linesOfInfo; i++) {
			musicList.Add (rdr.ReadLine ());
		}
		return (string)musicList [new System.Random ().Next (linesOfInfo)];
	}

	public Map loadMap (string mapName, string musicType)
	{
		StreamReader streamer = new StreamReader (@"Assets/Resources/Maps/" + mapName + @"/Stats.txt");
		string name = streamer.ReadLine ();
		int linesOfInfo = NumberConverter.ConvertToInt(streamer.ReadLine ().Substring(0, 3));
		string info = @"";
		for (int i = 0; i < linesOfInfo; i++) {
			info += streamer.ReadLine ();
			if (i + 1 != linesOfInfo) {
				info += @"" + '\n';
			}
		}
		string stageMusic = streamer.ReadLine ();
		int r, c;
		int rows = NumberConverter.ConvertToInt(streamer.ReadLine().Substring(0, 3));
		int columns = NumberConverter.ConvertToInt(streamer.ReadLine().Substring(0, 3));
		string[] traits = new string[NumberConverter.ConvertToInt(streamer.ReadLine().Substring(0, 3))];
		for (int i = 0; i < traits.Length; i++) {
			traits [i] = streamer.ReadLine();
		}


		Map map = new Map (name, mapName, info, stageMusic, rows, columns, traits);
		if (musicType.Equals (@"stage") || musicType.Equals (@"")) {
			map.Music = stageMusic;
		} else if (musicType.Equals (@"random")) {
			map.Music = loadRandomMusic ();
		} else {
			map.Music = musicType;
			//map.Music = @"ken";
		}

		MapObject obj;
		string loc;
		int numObjects = NumberConverter.ConvertToInt(streamer.ReadLine().Substring(0, 3));
		for (int i = 0; i < numObjects; i++) {
			obj = loadObject (streamer.ReadLine (), map.MapOwner);
			loc = streamer.ReadLine ();
		
			obj.changeLocation (
				new Location (NumberConverter.ConvertToInt (loc.Substring (0, loc.IndexOf (':'))),
				NumberConverter.ConvertToInt (loc.Substring (loc.IndexOf (':') + 1))));
				
			map.addObject (obj);
		}

		Skill s;
		int numSkills = NumberConverter.ConvertToInt(streamer.ReadLine().Substring(0, 3));
		for (int i = 0; i < numSkills; i++) {
			s = loadSkill (map.MapOwner, mapName, streamer.ReadLine(), @"Map", null);
			map.MapOwner.learn (s, false, -1);
			loc = streamer.ReadLine ();

			if (loc.Contains (@"???")) {
				map.TrapsToAdd.Add (s);
			} else {
				r = NumberConverter.ConvertToInt (loc.Substring (0, loc.IndexOf (':')));
				c = NumberConverter.ConvertToInt (loc.Substring (loc.IndexOf (':') + 1));

				map.TrapsAdded.Add (s);
				map.LocsAdded.Add (new Location (r, c));
			}
		}

		int[][] heightMap = new int[rows][];

		string output = streamer.ReadLine ();

		if (output.Contains (@"STOP")) {

			for (int i = 0; i < map.Rows; i++) {
				heightMap [i] = new int[columns];
				for (int j = 0; j < map.Columns; j++) {

					heightMap [i] [j] = 0;
				}
			}
		} else {
			for (int i = 0; i < map.Rows; i++) {
				heightMap [i] = new int[columns];
				for (int j = 0; j < map.Columns; j++) {

					string ln = streamer.ReadLine ();

					if (ln == null) {
						throw new NullReferenceException (i + @" " + j);
					}

					heightMap [i] [j] = NumberConverter.ConvertToInt (ln.Substring (0, 3));
				}
			}
		}

		map.HeightMap = heightMap;

		streamer.BaseStream.Position = 0;
		streamer.DiscardBufferedData ();
		streamer.Close ();
		return map;
	}

	private Boolean skillIsMatched (Skill leftHandSkill, Skill rightHandSkill)
	{
		return (rightHandSkill.SearchName.Equals (leftHandSkill.SearchName))
			|| (rightHandSkill.Properties.Contains (@"PAIRED∆") && leftHandSkill.Properties.Contains (@"PAIRED∆"))
			&& rightHandSkill.Properties.Substring (rightHandSkill.Properties.IndexOf ('∆') + 1).Equals
			(leftHandSkill.Properties.Substring (leftHandSkill.Properties.IndexOf ('∆') + 1));
	}

	private Boolean skillAlreadyInList (Player player, Skill rightHandSkill, Skill leftHandSkill, Boolean tree, StreamWriter writer, int numTabs)
	{
		string tab = @"";
		for (int i = 0; i < numTabs; i++) {
			tab += @"     ";
		}
		//writer.WriteLine (tab + player.FirstName + @" Checking if " + leftHandSkill.Name + @" can link to " + rightHandSkill.Name + " (@" + rightHandSkill.linkSkills.Count + ")");

		if (skillIsMatched (leftHandSkill, rightHandSkill)) {
			//writer.WriteLine (tab + "BAD - " + leftHandSkill.Name + " IS already linked to " + rightHandSkill.Name);
			return true;
		}
		for (int i = 0; i < rightHandSkill.LinkSkills.Count; i++) {
			if (skillIsMatched ((Skill)rightHandSkill.LinkSkills [i], rightHandSkill)) {
				//writer.WriteLine (tab + "BAD - " + leftHandSkill.Name + " IS already linked to " + rightHandSkill.Name);
				return true;
			}

			if (leftHandSkill.MasterSkill != null) {
				if (skillAlreadyInList (player, ((Skill)rightHandSkill.LinkSkills [i]), leftHandSkill.MasterSkill, tree, writer, numTabs + 1)) {
					//writer.WriteLine (tab + "GOOD - " + leftHandSkill.Name + " NOT already linked to " + ((Skill)rightHandSkill.LinkSkills [i]).Name);
					return true;
				}
			}
		
			if (tree) {
				//writer.WriteLine (tab + "BRANCHING FROM " + string.Format (@"{0} ({1}/{2})", rightHandSkill.Name, i + 1, rightHandSkill.linkSkills.Count));
				if (skillAlreadyInList (player, (Skill)rightHandSkill.LinkSkills [i], leftHandSkill, tree, writer, numTabs + 1)) {
					//writer.WriteLine (tab + "GOOD - " + leftHandSkill.Name + " NOT already linked to " + ((Skill)rightHandSkill.LinkSkills [i]).Name);
					return true;
				}
			}
		}
		//writer.WriteLine (tab + "GOOD - " + leftHandSkill.Name + " NOT already linked to " + rightHandSkill.Name);
		return false;
	}
		
	public void setLinkedSkillsByText (Player player, string key, string notText, ArrayList leftHandList, ArrayList rightHandList, StreamWriter writer, Boolean clearLinks)
	{
		writer.WriteLine ( player.FirstName + " Adding all " + key + " skills" + '\n');
		Skill leftHandSkill, rightHandSkill;
		for (int i = 0; i < leftHandList.Count; i ++) {
			leftHandSkill = (Skill)leftHandList [i];
			writer.WriteLine (@"---" + leftHandSkill.Name.ToUpper () + string.Format (@" ({0})", key) + "---");

			if (leftHandSkill.AddLink && leftHandSkill.Properties.Contains (key) && !leftHandSkill.AddLink && leftHandSkill.Properties.Contains (notText)) {

				if (clearLinks) {
					leftHandSkill.LinkSkills.Clear ();
				}

				for (int j = 0; j < rightHandList.Count; j++) {
					rightHandSkill = (Skill)rightHandList [j];

					if (rightHandSkill == null) {
						throw new NullReferenceException (player.Name);
					}

					writer.WriteLine (@"***" + rightHandSkill.Name.ToUpper () + "***");
					if (!rightHandSkill.Grapple && leftHandSkill.HitStun + player.HitStunAdjustment >= rightHandSkill.Speed
						&& !leftHandSkill.SearchName.Equals (rightHandSkill.SearchName)
						&& (leftHandSkill.LinkSkills.Count <= 0 || leftHandSkill.LinkSkills [0] == null || !((Skill)leftHandSkill.LinkSkills [0]).SearchName.Equals (rightHandSkill.SearchName))
						&& !skillAlreadyInList (player, rightHandSkill, leftHandSkill, true, writer, 0)) {
						writer.WriteLine (player.FirstName + " LINKING " + leftHandSkill.SearchName + " to " + rightHandSkill.SearchName + '\n');
						leftHandSkill.LinkSkill (rightHandSkill);
					} else {
						writer.WriteLine (@"");
					}
				}
			}
		}

		writer.WriteLine (@"---------------------" + player.FirstName + " " + key + " process completed " + '\n');
		//list.Clear ();

	}

	public void setLinkedSkillsByText (Player player, string key, ArrayList leftHandList, ArrayList rightHandList, StreamWriter writer, Boolean clearLinks)
	{
		writer.WriteLine ( player.FirstName + " Adding all " + key + " skills" + '\n');
		Skill leftHandSkill, rightHandSkill;
		for (int i = 0; i < leftHandList.Count; i ++) {
			leftHandSkill = (Skill)leftHandList [i];
			writer.WriteLine (@"---" + leftHandSkill.Name.ToUpper () + string.Format (@" ({0})", key) + "---");

			if (leftHandSkill.AddLink && leftHandSkill.Properties.Contains (key)) {
			
				if (clearLinks) {
					leftHandSkill.LinkSkills.Clear ();
				}

				for (int j = 0; j < rightHandList.Count; j++) {
					rightHandSkill = (Skill)rightHandList [j];

					if (rightHandSkill == null) {
						throw new NullReferenceException (player.Name);
					}

					writer.WriteLine (@"***" + rightHandSkill.Name.ToUpper () + "***");
					if (!rightHandSkill.Grapple && leftHandSkill.HitStun + player.HitStunAdjustment >= rightHandSkill.Speed
					    && !leftHandSkill.SearchName.Equals (rightHandSkill.SearchName)
					    && (leftHandSkill.LinkSkills.Count <= 0 || leftHandSkill.LinkSkills [0] == null || !((Skill)leftHandSkill.LinkSkills [0]).SearchName.Equals (rightHandSkill.SearchName))
					    && !skillAlreadyInList (player, rightHandSkill, leftHandSkill, true, writer, 0)) {
						writer.WriteLine (player.FirstName + " LINKING " + leftHandSkill.SearchName + " to " + rightHandSkill.SearchName + '\n');
						leftHandSkill.LinkSkill (rightHandSkill);
					} else {
						writer.WriteLine (@"");
					}
				}
			}
		}

		writer.WriteLine (@"---------------------" + player.FirstName + " " + key + " process completed " + '\n');
		//list.Clear ();
	}

	public void printOutputs (Team t1, Team t2, string mapName, Boolean appending, string text)
	{
		StreamWriter skillLogger = new StreamWriter (@"Assets/Resources/MatchInfo/" 
			+ t1.BasicTeamOutput + "VS" + t2.BasicTeamOutput + '@' + mapName + ".txt", appending);
		StreamWriter skillLogger2 = new StreamWriter (@"Assets/Resources/Data/Current/MatchToText.txt", appending);
		
		skillLogger.Write (text);
		skillLogger.Close ();
		skillLogger2.Write (text);
		skillLogger2.Close ();

	}

	public void calibrateLinks (Player player, Boolean clearLinks)
	{
		StreamWriter skillLogKeeper = new StreamWriter (@"Assets/Resources/MatchInfo/SkillLinkLogger/" + player.SearchName + ".txt");
		setLinkedSkillsByText (player, @"JMPS ", player.jumpAddedSkills, player.JumpSkills, skillLogKeeper, clearLinks);
		setLinkedSkillsByText (player, @"NORM ", player.normalAddedSkills, player.NormalSkills, skillLogKeeper, clearLinks);
		setLinkedSkillsByText (player, @"SPEC ", player.specialAddedSkills, player.SpecialSkills, skillLogKeeper, clearLinks);
		setLinkedSkillsByText (player, @"VITA ", player.vitalityAddedSkills, player.VitalitySkills, skillLogKeeper, clearLinks);
		setLinkedSkillsByText (player, @"BRST ", player.burstAddedSkills, player.BurstSkills, skillLogKeeper, clearLinks);
		setLinkedSkillsByText (player, @"ITMS ", player.itemAddedSkills, player.JumpSkills, skillLogKeeper, clearLinks);
		//setLinkedSkillsByText (player, @"SPEC ", player.sactionAddedSkills, new ArrayList (){player.SActionSkill}, skillLogKeeper, clearLinks);
		//setLinkedSkillsByText (player, @"SACT ", @"SPEC ", player.sactionAddedSkills, new ArrayList (){player.SActionSkill}, skillLogKeeper, clearLinks);
		setLinkedSkillsByText (player, @"SNGL ", player.singleAddedSkills, player.AllSkills, skillLogKeeper, clearLinks);

		setLinkedSkillsByText (player, @"JMPS2 ", player.jumpAddedSkills, player.JumpSkills, skillLogKeeper, clearLinks);
		setLinkedSkillsByText (player, @"NORM2 ", player.normalAddedSkills, player.NormalSkills, skillLogKeeper, clearLinks);
		setLinkedSkillsByText (player, @"SPEC2 ", player.specialAddedSkills, player.SpecialSkills, skillLogKeeper, clearLinks);
		setLinkedSkillsByText (player, @"VITA2 ", player.vitalityAddedSkills, player.VitalitySkills, skillLogKeeper, clearLinks);
		setLinkedSkillsByText (player, @"BRST2 ", player.burstAddedSkills, player.BurstSkills, skillLogKeeper, clearLinks);
		setLinkedSkillsByText (player, @"ITMS2 ", player.itemAddedSkills, player.JumpSkills, skillLogKeeper, clearLinks);
		//setLinkedSkillsByText (player, @"SPEC2 ", player.sactionAddedSkills, new ArrayList (){player.SActionSkill}, skillLogKeeper, clearLinks);
		//setLinkedSkillsByText (player, @"SACT2 ", @"SPEC ", player.sactionAddedSkills, new ArrayList (){player.SActionSkill}, skillLogKeeper, clearLinks);
		setLinkedSkillsByText (player, @"SNGL2 ", player.singleAddedSkills, player.AllSkills, skillLogKeeper, clearLinks);

		setLinkedSkillsByText (player, @"JMPS3 ", player.jumpAddedSkills, player.JumpSkills, skillLogKeeper, clearLinks);
		setLinkedSkillsByText (player, @"NORM3 ", player.normalAddedSkills, player.NormalSkills, skillLogKeeper, clearLinks);
		setLinkedSkillsByText (player, @"SPEC3 ", player.specialAddedSkills, player.SpecialSkills, skillLogKeeper, clearLinks);
		setLinkedSkillsByText (player, @"VITA3 ", player.vitalityAddedSkills, player.VitalitySkills, skillLogKeeper, clearLinks);
		setLinkedSkillsByText (player, @"BRST3 ", player.burstAddedSkills, player.BurstSkills, skillLogKeeper, clearLinks);
		setLinkedSkillsByText (player, @"ITMS3 ", player.itemAddedSkills, player.JumpSkills, skillLogKeeper, clearLinks);
		//setLinkedSkillsByText (player, @"SPEC3 ", player.sactionAddedSkills, new ArrayList (){player.SActionSkill}, skillLogKeeper, clearLinks);
		//setLinkedSkillsByText (player, @"SACT3 ", @"SPEC ", player.sactionAddedSkills, new ArrayList (){player.SActionSkill}, skillLogKeeper, clearLinks);
		setLinkedSkillsByText (player, @"SNGL3 ", player.singleAddedSkills, player.AllSkills, skillLogKeeper, clearLinks);

		printSkillTrees (player.AllSkills, skillLogKeeper, 0);

		skillLogKeeper.Close ();
	}

	public void loadSkills (Player player, string type)
	{
		StreamReader skillReader = null;
		if (type.Equals (@"Player")) {
			skillReader = new StreamReader (@"Assets/Resources/Players/" + player.SearchName + "/Skillset.txt");
		} else {
			skillReader = new StreamReader (@"Assets/Resources/Maps/" + player.SearchName + "/" + player.SearchName + "/Skillset.txt");
		}

		int numSkills = NumberConverter.ConvertToInt (skillReader.ReadLine().Substring(0, 3));
		for (int i = 0; i < numSkills; i++) {
			player.learn (loadSkill (player, player.SearchName, skillReader.ReadLine(), type, null), false, -1);
		}

		player.learn (loadSkill (player, @"Crash", null), false, -1);

		calibrateLinks (player, false);

		skillReader.BaseStream.Position = 0;
		skillReader.DiscardBufferedData ();
		skillReader.Close ();
	}

	private void printSkillTrees (ArrayList skillSet, StreamWriter writer, int numTabs)
	{
		Skill s;
		string tab = @"";
		for (int i = 0; i < numTabs; i++) {
			tab += @"     ";
		}

		for (int i = 0; i < skillSet.Count; i++) {
			s = (Skill)skillSet [i];
			if (i == 0) {
				writer.Write (@"*");
			}
			writer.WriteLine (tab + s.Name);
			printSkillTrees (s.LinkSkills, writer, numTabs + 1);		
		}
	}

	public void loadItems (Player player)
	{
		StreamReader skillReader = new StreamReader (@"Assets/Resources/Players/" + player.SearchName + @"/Equipment.txt");
		int numItems = NumberConverter.ConvertToInt (skillReader.ReadLine().Substring(0, 3));
		for (int i = 0; i < numItems; i++) {
			player.Inventory.Add (loadItem (player.SearchName, player.NoAmmo, skillReader.ReadLine()));
		}

		skillReader.BaseStream.Position = 0;
		skillReader.DiscardBufferedData ();
		skillReader.Close ();
	}

	public void loadInventory (Team t)
	{
		t.Inventory.Add (loadTeamInventory (@"HPUp"));
		t.Inventory.Add (loadTeamInventory (@"HPUpHigh"));
		t.Inventory.Add (loadTeamInventory (@"RMUp"));
		t.Inventory.Add (loadTeamInventory (@"RMUpHigh"));
		t.Inventory.Add (loadTeamInventory (@"GMUp"));
		t.Inventory.Add (loadTeamInventory (@"VMUp"));
		t.Inventory.Add (loadTeamInventory (@"Antidote"));
		t.Inventory.Add (loadTeamInventory (@"StunDown"));
		t.Inventory.Add (loadTeamInventory (@"Able"));
		t.Inventory.Add (loadTeamInventory (@"Goblet"));
		t.Inventory.Add (loadTeamInventory (@"Salts"));
		t.Inventory.Add (loadTeamInventory (@"Revive"));
		t.Inventory.Add (loadTeamInventory (@"TeamNotes"));
		t.Inventory.Add (loadTeamInventory (@"SummoningStone"));
	}

	public Skill loadTeamInventory (string skillName)
	{
		StreamReader skillReader = new StreamReader (@"Assets/Resources/Universal/Inventory/" + skillName + ".txt");
		string nextLine = @"";

		string name = skillReader.ReadLine ();
		string description = skillReader.ReadLine();
		string properties = skillReader.ReadLine ();
		string type = skillReader.ReadLine ();

		string[] conditions = new string[NumberConverter.ConvertToInt (skillReader.ReadLine().Substring(0, 3))];
		for (int i = 0; i < conditions.Length; i++) {
			conditions [i] = skillReader.ReadLine ();
		}
		string[] phrases = new string[NumberConverter.ConvertToInt (skillReader.ReadLine().Substring(0, 3))];
		//Console.WriteLine (skillName + " " + phrases.Length);
		for (int i = 0; i < phrases.Length; i++) {
			phrases [i] = skillReader.ReadLine ();
		}

		//string[][] specificPhrases = new string[NumberConverter.ConvertToInt (skillReader.ReadLine().Substring(0, 3))][];
		Int16 length;
		Int16.TryParse (skillReader.ReadLine().Substring(0, 3), out length);
		string[][] specificPhrases = new string[length][];
		//Console.WriteLine (skillName + " " + specificPhrases.Length);
		for (int i = 0; i < specificPhrases.Length; i++) {
			nextLine = skillReader.ReadLine ();
			specificPhrases[i] = new string[2];
			specificPhrases [i] [0] = nextLine.Substring(0, nextLine.IndexOf(':'));
			specificPhrases [i] [1] = nextLine.Substring (nextLine.IndexOf(':') + 1);
		}

		int range = NumberConverter.ConvertToInt (skillReader.ReadLine().Substring(0, 3));
		int speed = NumberConverter.ConvertToInt (skillReader.ReadLine().Substring(0, 3));
		int hitStun = NumberConverter.ConvertToInt (skillReader.ReadLine().Substring(0, 3));
		int motionRange = NumberConverter.ConvertToInt (skillReader.ReadLine().Substring(0, 3));

		nextLine = skillReader.ReadLine ();
		State force = new State(@"Force", @"FRC", NumberConverter.ConvertToInt(nextLine.Substring(0, 3)),

			0.0, 0, Convert.ToDouble(nextLine.Substring(4, 6)), false, @"is knocked in a direction!");
		int widthOrRadius = NumberConverter.ConvertToInt (skillReader.ReadLine().Substring(0, 3));
		Double ratio = Convert.ToDouble (skillReader.ReadLine().Substring(0, 6));
		Double hitRate = Convert.ToDouble (skillReader.ReadLine().Substring(0, 6));
		int reps = NumberConverter.ConvertToInt (skillReader.ReadLine().Substring(0, 3));

		Boolean[] links = new bool[NumberConverter.ConvertToInt(skillReader.ReadLine().Substring(0, 3))];
		for (int i = 0; i < links.Length; i++) {
			if (links.Length != reps) {
				throw new NullReferenceException (name);
			}
			links [i] = skillReader.ReadLine ().ToLower().Equals (@"true");
		}

		int threshold = NumberConverter.ConvertToInt (skillReader.ReadLine().Substring(0, 3));
		int numUses = NumberConverter.ConvertToInt (skillReader.ReadLine().Substring(0, 3));
		int recovery = NumberConverter.ConvertToInt (skillReader.ReadLine().Substring(0, 3));

		int[] cost = new int[4];
		int[] change = new int[4];

		/**
			 *
			 * METER COSTS
			 *
			 */

		cost [0] = NumberConverter.ConvertToInt (skillReader.ReadLine().Substring(0, 6));
		cost [1] = NumberConverter.ConvertToInt (skillReader.ReadLine().Substring(0, 6));
		cost [2] = NumberConverter.ConvertToInt (skillReader.ReadLine().Substring(0, 6));
		cost [3] = NumberConverter.ConvertToInt (skillReader.ReadLine().Substring(0, 6));

		/**
			 *
			 * METER AUGMENTS
			 *
			 */

		change [0] = NumberConverter.ConvertToInt (skillReader.ReadLine().Substring(0, 6));
		change [1] = NumberConverter.ConvertToInt (skillReader.ReadLine().Substring(0, 6));
		change [2] = NumberConverter.ConvertToInt (skillReader.ReadLine().Substring(0, 6));
		change [3] = NumberConverter.ConvertToInt (skillReader.ReadLine().Substring(0, 6));

		/**
			 *
			 * STAT AUGMENTS
			 *
			 */

		State[] statAugments = new State[10];

		nextLine = skillReader.ReadLine ();
		statAugments [0] = new State (@"Strength", @"STR", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();

		statAugments [1] = new State (@"Grit", @"GRT", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();

		statAugments [2] = new State (@"Magick", @"MAG", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();

		statAugments [3] = new State (@"Resistance", @"RES", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();

		statAugments [4] = new State (@"Dexterity", @"DEX", 0, Convert.ToDouble(nextLine.Substring(0, 6)),
			NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();

		statAugments [5] = new State (@"Speed", @"SPD", 0, Convert.ToDouble(nextLine.Substring(0, 6)),
			NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();

		statAugments [6] = new State (@"Proration", @"PRO", 0, Convert.ToDouble(nextLine.Substring(0, 6)),
			NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();

		statAugments [7] = new State (@"Movement", @"MOV", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();

		statAugments [8] = new State (@"Teamwork", @"TWK", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();

		statAugments [9] = new State (@"Luck", @"LCK", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");


		/**
			 * 
			 * ELEMENTS
			 *
			 */

		State[] elements = new State[10];
		nextLine = skillReader.ReadLine ();
		elements [0] = new State (@"Fire", @"FIR", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		elements [1] = new State (@"Ice", @"ICE", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		elements [2] = new State (@"Electricity", @"ELE", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		elements [3] = new State (@"Wind", @"WIN", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		elements [4] = new State (@"Water", @"WAT", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		elements [5] = new State (@"Earth", @"EAR", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		elements [6] = new State (@"Metal", @"MET", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		elements [7] = new State (@"Darkness", @"DAR", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		elements [8] = new State (@"Light", @"LIG", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		elements [9] = new State (@"Break", @"BRK", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");


		/**
			 * 
			 * STATE AUGMENTS
			 * 
			 */

		State[] stateAugments = new State[20];

		nextLine = skillReader.ReadLine ();
		stateAugments [0] = new State (@"Stun Hit", @"STN", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		stateAugments [1] = new State (@"Grounded", @"GND", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		stateAugments [2] = new State (@"Airborne", @"AIR", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		stateAugments [3] = new State (@"Counter", @"CTR", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		stateAugments [4] = new State (@"Parry", @"PAR", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		stateAugments [5] = new State (@"Poison", @"PSN", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		stateAugments [6] = new State (@"Regen", @"REG", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		stateAugments [7] = new State (@"Daze", @"DZE", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		stateAugments [8] = new State (@"Confuse", @"CNF", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		stateAugments [9] = new State (@"Sadness", @"SAD", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		stateAugments [10] = new State (@"Fury", @"FUR", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		stateAugments [11] = new State (@"Sleep", @"SLP", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		stateAugments [12] = new State (@"Adle", @"ADL", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		stateAugments [13] = new State (@"Freeze", @"FRZ", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		stateAugments [14] = new State (@"Immune", @"IMM", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		stateAugments [15] = new State (@"Invulnerable", @"INV", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		stateAugments [16] = new State (@"Burn", @"BRN", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		stateAugments [17] = new State (@"Learn", @"LRN", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		stateAugments [18] = new State (@"Invisible", @"INI", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		stateAugments [19] = new State (@"Leech", @"LCH", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");


		//EXTRA STATES
		ArrayList extraStateAugments = new ArrayList ();
		if (type.Contains (@"BLIND ")) {
			extraStateAugments.Add (new State (@"Blind", @"BLD", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
				0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
				Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!"));
		}

		DynamicDouble[] stateResistances = new DynamicDouble [20];
		for (int i = 0; i < stateResistances.Length; i++) {
			stateResistances [i] = new DynamicDouble (@"N/A", @"N/A", 0.0);
		}

		if (properties.Contains (@"STUNRES")) {
			nextLine = skillReader.ReadLine ();
			stateResistances [0] = new DynamicDouble (@"Stun Hit", @"STN",
				Convert.ToDouble(nextLine.Substring(0, 6)));
			stateResistances [0].Holder = Convert.ToDouble (nextLine.Substring(7, 6));
			//Console.WriteLine (@"TEST: " + stateResistances[0].Name + " " + stateResistances[0].BaseValue + " " + stateResistances[0].Holder);
		}

		if (properties.Contains (@"GROUNDEDRES")) {
			nextLine = skillReader.ReadLine ();
			stateResistances [1] = new DynamicDouble (@"Grounded", @"GND",
				Convert.ToDouble(nextLine.Substring(0, 6)));
			stateResistances [1].Holder = Convert.ToDouble (nextLine.Substring(7, 6));
			//Console.WriteLine (@"TEST: " + stateResistances[1].Name + " " + stateResistances[1].BaseValue + " " + stateResistances[1].Holder);
		}

		if (properties.Contains (@"AIRBORNERES")) {
			nextLine = skillReader.ReadLine ();
			stateResistances [2] = new DynamicDouble (@"Airborne", @"AIR",
				Convert.ToDouble(nextLine.Substring(0, 6)));
			stateResistances [2].Holder = Convert.ToDouble (nextLine.Substring(7, 6));
			//Console.WriteLine (@"TEST: " + stateResistances[2].Name + " " + stateResistances[2].BaseValue + " " + stateResistances[2].Holder);
		}

		if (properties.Contains (@"COUNTERRES")) {
			nextLine = skillReader.ReadLine ();
			stateResistances [3] = new DynamicDouble (@"Counter", @"STN",
				Convert.ToDouble(nextLine.Substring(0, 6)));
			stateResistances [3].Holder = Convert.ToDouble (nextLine.Substring(7, 6));
			//Console.WriteLine (@"TEST: " + stateResistances[3].Name + " " + stateResistances[3].BaseValue + " " + stateResistances[3].Holder);
		}

		if (properties.Contains (@"PARRYRES")) {
			nextLine = skillReader.ReadLine ();
			stateResistances [4] = new DynamicDouble (@"Parry", @"STN",
				Convert.ToDouble(nextLine.Substring(0, 6)));
			stateResistances [4].Holder = Convert.ToDouble (nextLine.Substring(7, 6));
			//Console.WriteLine (@"TEST: " + stateResistances[4].Name + " " + stateResistances[4].BaseValue + " " + stateResistances[4].Holder);
		}

		if (properties.Contains (@"POISONRES")) {
			nextLine = skillReader.ReadLine ();
			stateResistances [5] = new DynamicDouble (@"Poison", @"STN",
				Convert.ToDouble(nextLine.Substring(0, 6)));
			stateResistances [5].Holder = Convert.ToDouble (nextLine.Substring(7, 6));
			//Console.WriteLine (@"TEST: " + stateResistances[5].Name + " " + stateResistances[5].BaseValue + " " + stateResistances[5].Holder);
		}

		if (properties.Contains (@"REGENRES")) {
			nextLine = skillReader.ReadLine ();
			stateResistances [6] = new DynamicDouble (@"Regen", @"GND",
				Convert.ToDouble(nextLine.Substring(0, 6)));
			stateResistances [6].Holder = Convert.ToDouble (nextLine.Substring(7, 6));
			//Console.WriteLine (@"TEST: " + stateResistances[6].Name + " " + stateResistances[6].BaseValue + " " + stateResistances[6].Holder);
		}

		if (properties.Contains (@"DAZERES")) {
			nextLine = skillReader.ReadLine ();
			stateResistances [7] = new DynamicDouble (@"Daze", @"AIR",
				Convert.ToDouble(nextLine.Substring(0, 6)));
			stateResistances [7].Holder = Convert.ToDouble (nextLine.Substring(7, 6));
			//Console.WriteLine (@"TEST: " + stateResistances[7].Name + " " + stateResistances[7].BaseValue + " " + stateResistances[7].Holder);
		}

		if (properties.Contains (@"CONFUSERES")) {
			nextLine = skillReader.ReadLine ();
			stateResistances [8] = new DynamicDouble (@"Confuse", @"STN",
				Convert.ToDouble(nextLine.Substring(0, 6)));
			stateResistances [8].Holder = Convert.ToDouble (nextLine.Substring(7, 6));
			//Console.WriteLine (@"TEST: " + stateResistances[8].Name + " " + stateResistances[8].BaseValue + " " + stateResistances[8].Holder);
		}

		if (properties.Contains (@"SADNESSRES")) {
			nextLine = skillReader.ReadLine ();
			stateResistances [9] = new DynamicDouble (@"Sadness", @"STN",
				Convert.ToDouble(nextLine.Substring(0, 6)));
			stateResistances [9].Holder = Convert.ToDouble (nextLine.Substring(7, 6));
			//Console.WriteLine (@"TEST: " + stateResistances[9].Name + " " + stateResistances[9].BaseValue + " " + stateResistances[9].Holder);
		}

		if (properties.Contains (@"FURYRES")) {
			nextLine = skillReader.ReadLine ();
			stateResistances [10] = new DynamicDouble (@"Fury", @"STN",
				Convert.ToDouble(nextLine.Substring(0, 6)));
			stateResistances [10].Holder = Convert.ToDouble (nextLine.Substring(7, 6));
			//Console.WriteLine (@"TEST: " + stateResistances[10].Name + " " + stateResistances[10].BaseValue + " " + stateResistances[10].Holder);
		}

		if (properties.Contains (@"SLEEPRES")) {
			nextLine = skillReader.ReadLine ();
			stateResistances [11] = new DynamicDouble (@"Sleep", @"GND",
				Convert.ToDouble(nextLine.Substring(0, 6)));
			stateResistances [11].Holder = Convert.ToDouble (nextLine.Substring(7, 6));
			//Console.WriteLine (@"TEST: " + stateResistances[11].Name + " " + stateResistances[11].BaseValue + " " + stateResistances[11].Holder);
		}

		if (properties.Contains (@"ADLERES")) {
			nextLine = skillReader.ReadLine ();
			stateResistances [12] = new DynamicDouble (@"Adle", @"AIR",
				Convert.ToDouble(nextLine.Substring(0, 6)));
			stateResistances [12].Holder = Convert.ToDouble (nextLine.Substring(7, 6));
			//Console.WriteLine (@"TEST: " + stateResistances[12].Name + " " + stateResistances[12].BaseValue + " " + stateResistances[12].Holder);
		}

		if (properties.Contains (@"FREEZERES")) {
			nextLine = skillReader.ReadLine ();
			stateResistances [13] = new DynamicDouble (@"Freeze", @"STN",
				Convert.ToDouble(nextLine.Substring(0, 6)));
			stateResistances [13].Holder = Convert.ToDouble (nextLine.Substring(7, 6));
			//Console.WriteLine (@"TEST: " + stateResistances[13].Name + " " + stateResistances[13].BaseValue + " " + stateResistances[13].Holder);
		}

		if (properties.Contains (@"IMMUNERES")) {
			nextLine = skillReader.ReadLine ();
			stateResistances [14] = new DynamicDouble (@"Immune", @"STN",
				Convert.ToDouble(nextLine.Substring(0, 6)));
			stateResistances [14].Holder = Convert.ToDouble (nextLine.Substring(7, 6));
			//Console.WriteLine (@"TEST: " + stateResistances[14].Name + " " + stateResistances[14].BaseValue + " " + stateResistances[14].Holder);
		}

		if (properties.Contains (@"INVULNERABLERES")) {
			nextLine = skillReader.ReadLine ();
			stateResistances [15] = new DynamicDouble (@"Invulnerable", @"STN",
				Convert.ToDouble(nextLine.Substring(0, 6)));
			stateResistances [15].Holder = Convert.ToDouble (nextLine.Substring(7, 6));
			//Console.WriteLine (@"TEST: " + stateResistances[15].Name + " " + stateResistances[15].BaseValue + " " + stateResistances[15].Holder);
		}

		if (properties.Contains (@"BURNRES")) {
			nextLine = skillReader.ReadLine ();
			stateResistances [16] = new DynamicDouble (@"Burn", @"GND",
				Convert.ToDouble(nextLine.Substring(0, 6)));
			stateResistances [16].Holder = Convert.ToDouble (nextLine.Substring(7, 6));
			//Console.WriteLine (@"TEST: " + stateResistances[16].Name + " " + stateResistances[16].BaseValue + " " + stateResistances[16].Holder);
		}

		if (properties.Contains (@"LEARNRES")) {
			nextLine = skillReader.ReadLine ();
			stateResistances [17] = new DynamicDouble (@"Learn", @"AIR",
				Convert.ToDouble(nextLine.Substring(0, 6)));
			stateResistances [17].Holder = Convert.ToDouble (nextLine.Substring(7, 6));
			//Console.WriteLine (@"TEST: " + stateResistances[17].Name + " " + stateResistances[17].BaseValue + " " + stateResistances[17].Holder);
		}

		if (properties.Contains (@"INVISIBLERES")) {
			nextLine = skillReader.ReadLine ();
			stateResistances [18] = new DynamicDouble (@"Invisible", @"STN",
				Convert.ToDouble(nextLine.Substring(0, 6)));
			stateResistances [18].Holder = Convert.ToDouble (nextLine.Substring(7, 6));
			//Console.WriteLine (@"TEST: " + stateResistances[18].Name + " " + stateResistances[18].BaseValue + " " + stateResistances[18].Holder);
		}

		if (properties.Contains (@"LEECHRES")) {
			nextLine = skillReader.ReadLine ();
			stateResistances [19] = new DynamicDouble (@"Leech", @"STN",
				Convert.ToDouble(nextLine.Substring(0, 6)));
			stateResistances [19].Holder = Convert.ToDouble (nextLine.Substring(7, 6));
			//Console.WriteLine (@"TEST: " + stateResistances[19].Name + " " + stateResistances[19].BaseValue + " " + stateResistances[19].Holder);
		}

		Skill s = new Skill (name, skillName, description, properties, type, conditions, phrases,
			specificPhrases, range, speed, hitStun, motionRange, force, widthOrRadius, ratio, hitRate, reps, links, threshold, numUses,
			recovery, cost, change, statAugments, stateAugments, extraStateAugments, elements, stateResistances,
			new ArrayList (), null, null, 0, null, null, new ArrayList (), new ArrayList (), new ArrayList (), null, null);


		int linkSkills = NumberConverter.ConvertToInt (skillReader.ReadLine().Substring(0, 3));

        /**
		if (properties.Contains(@"PCANCEL")) {
			s.CancelSkill = loadSkill (null, @"Cancel", s);
		}
        */

		if (stateAugments [3].Potency != 0) {
			s.CounterSkill = loadTeamInventory (skillReader.ReadLine());
		}

		s.Timer = NumberConverter.ConvertToInt(skillReader.ReadLine().Substring(0, 3));
		if (s.Timer != 0 && (!properties.Contains(@"TIMESTOP ") || type.Contains (@"TIME"))) {
			s.TimerSkill = loadTeamInventory (skillReader.ReadLine ());
		}

		if (type.Contains (@"OBJECT ")) {
			s.TrapSkill = loadTeamInventory (skillReader.ReadLine ());
		}

		if (type.Contains (@"ADDSKL ")) {
			int lrn = NumberConverter.ConvertToInt(skillReader.ReadLine().Substring(0, 3));

			for (int i = 0; i < lrn; i ++)  {
				s.LearnSkills.Add (loadTeamInventory (skillReader.ReadLine ()));
			}
		}

		if (type.Contains (@"FORGETSKL ")) {
			int forg = NumberConverter.ConvertToInt(skillReader.ReadLine().Substring(0, 3));

			for (int i = 0; i < forg; i ++)  {
				s.ForgetSkills.Add (skillReader.ReadLine ());
			}
		}

		if (properties.Contains (@"DROP")) {
			
			s.DropItem = loadTeamInventory (skillReader.ReadLine ());
		}

		//if (x == null) {
		//	throw new NullReferenceException (@"DAMN'T");
		//}

		string inputReader = skillReader.ReadLine ();

		if (inputReader == null || inputReader.Substring (0, 3) == null) {
			throw new NullReferenceException (s.Name);
		}

		s.Inputs = new string[NumberConverter.ConvertToInt (inputReader.Substring(0, 3))];
		for (int inp = 0; inp < s.Inputs.Length; inp++) {
			s.Inputs [inp] = skillReader.ReadLine ();
		}

		skillReader.BaseStream.Position = 0;
		skillReader.DiscardBufferedData ();
		skillReader.Close ();
		return s;
	}

	public Skill loadGrab (Player p, string playerName, Boolean airGrab, int grabIndex)
	{
		StreamReader skillReader = null;
		if (!airGrab) {
			skillReader = new StreamReader (@"Assets/Resources/Universal/Skills/Grab.txt");
		} else {
			skillReader = new StreamReader (@"Assets/Resources/Universal/Skills/AirGrab" + grabIndex + ".txt");
		}

		string nextLine = @"";

		string name = skillReader.ReadLine ();
		string description = skillReader.ReadLine();
		string properties = skillReader.ReadLine ();
		string type = skillReader.ReadLine ();

		string[] conditions = new string[NumberConverter.ConvertToInt (skillReader.ReadLine().Substring(0, 3))];
		for (int i = 0; i < conditions.Length; i++) {
			conditions [i] = skillReader.ReadLine ();
		}
		string[] phrases = new string[NumberConverter.ConvertToInt (skillReader.ReadLine().Substring(0, 3))];
		for (int i = 0; i < phrases.Length; i++) {
			phrases [i] = skillReader.ReadLine ();
		}

		string[][] specificPhrases = new string[NumberConverter.ConvertToInt (skillReader.ReadLine().Substring(0, 3))][];
		for (int i = 0; i < specificPhrases.Length; i++) {
			nextLine = skillReader.ReadLine ();
			specificPhrases[i] = new string[2];
			specificPhrases [i] [0] = nextLine.Substring(0, nextLine.IndexOf(':'));
			specificPhrases [i] [1] = nextLine.Substring (nextLine.IndexOf(':') + 1);
		}

		int range = NumberConverter.ConvertToInt (skillReader.ReadLine().Substring(0, 3));
		int speed = NumberConverter.ConvertToInt (skillReader.ReadLine().Substring(0, 3));
		int hitStun = NumberConverter.ConvertToInt (skillReader.ReadLine().Substring(0, 3));
		int motionRange = NumberConverter.ConvertToInt (skillReader.ReadLine().Substring(0, 3));

		nextLine = skillReader.ReadLine ();
		State force = new State(@"Force", @"FRC", NumberConverter.ConvertToInt(nextLine.Substring(0, 3)),
			0.0, 0, Convert.ToDouble(nextLine.Substring(4, 6)), false, @"is augmented!");
		int widthOrRadius = NumberConverter.ConvertToInt (skillReader.ReadLine().Substring(0, 3));
		Double ratio = Convert.ToDouble (skillReader.ReadLine().Substring(0, 6));
		Double hitRate = Convert.ToDouble (skillReader.ReadLine().Substring(0, 6));
		int reps = NumberConverter.ConvertToInt (skillReader.ReadLine().Substring(0, 3));

		Boolean[] links = new bool[NumberConverter.ConvertToInt(skillReader.ReadLine().Substring(0, 3))];
		for (int i = 0; i < links.Length; i++) {
			links [i] = skillReader.ReadLine ().ToLower().Equals (@"true");
		}

		int threshold = NumberConverter.ConvertToInt (skillReader.ReadLine().Substring(0, 3));
		int numUses = NumberConverter.ConvertToInt (skillReader.ReadLine().Substring(0, 3));
		int recovery = NumberConverter.ConvertToInt (skillReader.ReadLine().Substring(0, 3));

		int[] cost = new int[4];
		int[] change = new int[4];

		/**
			 *
			 * METER COSTS
			 *
			 */

		cost [0] = NumberConverter.ConvertToInt (skillReader.ReadLine().Substring(0, 6));
		cost [1] = NumberConverter.ConvertToInt (skillReader.ReadLine().Substring(0, 6));
		cost [2] = NumberConverter.ConvertToInt (skillReader.ReadLine().Substring(0, 6));
		cost [3] = NumberConverter.ConvertToInt (skillReader.ReadLine().Substring(0, 6));

		/**
			 *
			 * METER AUGMENTS
			 *
			 */

		change [0] = NumberConverter.ConvertToInt (skillReader.ReadLine().Substring(0, 6));
		change [1] = NumberConverter.ConvertToInt (skillReader.ReadLine().Substring(0, 6));
		change [2] = NumberConverter.ConvertToInt (skillReader.ReadLine().Substring(0, 6));
		change [3] = NumberConverter.ConvertToInt (skillReader.ReadLine().Substring(0, 6));

		/**
			 *
			 * STAT AUGMENTS
			 *
			 */

		State[] statAugments = new State[10];

		nextLine = skillReader.ReadLine ();
		statAugments [0] = new State (@"Strength", @"STR", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		statAugments [1] = new State (@"Grit", @"GRT", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		statAugments [2] = new State (@"Magick", @"MAG", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		statAugments [3] = new State (@"Resistance", @"RES", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		statAugments [4] = new State (@"Dexterity", @"DEX", 0, Convert.ToDouble(nextLine.Substring(0, 6)),
			NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		statAugments [5] = new State (@"Speed", @"SPD", 0, Convert.ToDouble(nextLine.Substring(0, 6)),
			NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		statAugments [6] = new State (@"Proration", @"PRO", 0, Convert.ToDouble(nextLine.Substring(0, 6)),
			NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		statAugments [7] = new State (@"Movement", @"MOV", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		statAugments [8] = new State (@"Teamwork", @"TWK", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		statAugments [9] = new State (@"Luck", @"LCK", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");


		/**
			 * 
			 * ELEMENTS
			 *
			 */

		State[] elements = new State[10];
		nextLine = skillReader.ReadLine ();
		elements [0] = new State (@"Fire", @"FIR", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		elements [1] = new State (@"Ice", @"ICE", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		elements [2] = new State (@"Electricity", @"ELE", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		elements [3] = new State (@"Wind", @"WIN", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		elements [4] = new State (@"Water", @"WAT", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		elements [5] = new State (@"Earth", @"EAR", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		elements [6] = new State (@"Metal", @"MET", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		elements [7] = new State (@"Darkness", @"DAR", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		elements [8] = new State (@"Light", @"LIG", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		elements [9] = new State (@"Break", @"BRK", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");


		/**
			 * 
			 * STATE AUGMENTS
			 * 
			 */

		State[] stateAugments = new State[20];

		nextLine = skillReader.ReadLine ();
		stateAugments [0] = new State (@"Stun Hit", @"STN", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		stateAugments [1] = new State (@"Grounded", @"GND", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		stateAugments [2] = new State (@"Airborne", @"AIR", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		stateAugments [3] = new State (@"Counter", @"CTR", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		stateAugments [4] = new State (@"Parry", @"PAR", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		stateAugments [5] = new State (@"Poison", @"PSN", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		stateAugments [6] = new State (@"Regen", @"REG", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		stateAugments [7] = new State (@"Daze", @"DZE", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		stateAugments [8] = new State (@"Confuse", @"CNF", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		stateAugments [9] = new State (@"Sadness", @"SAD", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		stateAugments [10] = new State (@"Fury", @"FUR", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		stateAugments [11] = new State (@"Sleep", @"SLP", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		stateAugments [12] = new State (@"Adle", @"ADL", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		stateAugments [13] = new State (@"Freeze", @"FRZ", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		stateAugments [14] = new State (@"Immune", @"IMM", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		stateAugments [15] = new State (@"Invulnerable", @"INV", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		stateAugments [16] = new State (@"Burn", @"BRN", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		stateAugments [17] = new State (@"Learn", @"LRN", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		stateAugments [18] = new State (@"Invisible", @"INI", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		stateAugments [19] = new State (@"Leech", @"LCH", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		//EXTRA STATES
		ArrayList extraStateAugments = new ArrayList ();
		if (type.Contains (@"BLIND ")) {
			extraStateAugments.Add (new State (@"Blind", @"BLD", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
				0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
				Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!"));
		}


		DynamicDouble[] stateResistances = new DynamicDouble [20];
		for (int i = 0; i < stateResistances.Length; i++) {
			stateResistances [i] = new DynamicDouble (@"N/A", @"N/A", 0.0);
		}

		if (properties.Contains (@"STUNRES")) {
			nextLine = skillReader.ReadLine ();
			stateResistances [0] = new DynamicDouble (@"Stun Hit", @"STN",
				Convert.ToDouble(nextLine.Substring(0, 6)));
			stateResistances [0].Holder = Convert.ToDouble (nextLine.Substring(7, 6));
			//Console.WriteLine (@"TEST: " + stateResistances[0].Name + " " + stateResistances[0].BaseValue + " " + stateResistances[0].Holder);
		}

		if (properties.Contains (@"GROUNDEDRES")) {
			nextLine = skillReader.ReadLine ();
			stateResistances [1] = new DynamicDouble (@"Grounded", @"GND",
				Convert.ToDouble(nextLine.Substring(0, 6)));
			stateResistances [1].Holder = Convert.ToDouble (nextLine.Substring(7, 6));
			//Console.WriteLine (@"TEST: " + stateResistances[1].Name + " " + stateResistances[1].BaseValue + " " + stateResistances[1].Holder);
		}

		if (properties.Contains (@"AIRBORNERES")) {
			nextLine = skillReader.ReadLine ();
			stateResistances [2] = new DynamicDouble (@"Airborne", @"AIR",
				Convert.ToDouble(nextLine.Substring(0, 6)));
			stateResistances [2].Holder = Convert.ToDouble (nextLine.Substring(7, 6));
			//Console.WriteLine (@"TEST: " + stateResistances[2].Name + " " + stateResistances[2].BaseValue + " " + stateResistances[2].Holder);
		}

		if (properties.Contains (@"COUNTERRES")) {
			nextLine = skillReader.ReadLine ();
			stateResistances [3] = new DynamicDouble (@"Counter", @"STN",
				Convert.ToDouble(nextLine.Substring(0, 6)));
			stateResistances [3].Holder = Convert.ToDouble (nextLine.Substring(7, 6));
			//Console.WriteLine (@"TEST: " + stateResistances[3].Name + " " + stateResistances[3].BaseValue + " " + stateResistances[3].Holder);
		}

		if (properties.Contains (@"PARRYRES")) {
			nextLine = skillReader.ReadLine ();
			stateResistances [4] = new DynamicDouble (@"Parry", @"STN",
				Convert.ToDouble(nextLine.Substring(0, 6)));
			stateResistances [4].Holder = Convert.ToDouble (nextLine.Substring(7, 6));
			//Console.WriteLine (@"TEST: " + stateResistances[4].Name + " " + stateResistances[4].BaseValue + " " + stateResistances[4].Holder);
		}

		if (properties.Contains (@"POISONRES")) {
			nextLine = skillReader.ReadLine ();
			stateResistances [5] = new DynamicDouble (@"Poison", @"STN",
				Convert.ToDouble(nextLine.Substring(0, 6)));
			stateResistances [5].Holder = Convert.ToDouble (nextLine.Substring(7, 6));
			//Console.WriteLine (@"TEST: " + stateResistances[5].Name + " " + stateResistances[5].BaseValue + " " + stateResistances[5].Holder);
		}

		if (properties.Contains (@"REGENRES")) {
			nextLine = skillReader.ReadLine ();
			stateResistances [6] = new DynamicDouble (@"Regen", @"GND",
				Convert.ToDouble(nextLine.Substring(0, 6)));
			stateResistances [6].Holder = Convert.ToDouble (nextLine.Substring(7, 6));
			//Console.WriteLine (@"TEST: " + stateResistances[6].Name + " " + stateResistances[6].BaseValue + " " + stateResistances[6].Holder);
		}

		if (properties.Contains (@"DAZERES")) {
			nextLine = skillReader.ReadLine ();
			stateResistances [7] = new DynamicDouble (@"Daze", @"AIR",
				Convert.ToDouble(nextLine.Substring(0, 6)));
			stateResistances [7].Holder = Convert.ToDouble (nextLine.Substring(7, 6));
			//Console.WriteLine (@"TEST: " + stateResistances[7].Name + " " + stateResistances[7].BaseValue + " " + stateResistances[7].Holder);
		}

		if (properties.Contains (@"CONFUSERES")) {
			nextLine = skillReader.ReadLine ();
			stateResistances [8] = new DynamicDouble (@"Confuse", @"STN",
				Convert.ToDouble(nextLine.Substring(0, 6)));
			stateResistances [8].Holder = Convert.ToDouble (nextLine.Substring(7, 6));
			//Console.WriteLine (@"TEST: " + stateResistances[8].Name + " " + stateResistances[8].BaseValue + " " + stateResistances[8].Holder);
		}

		if (properties.Contains (@"SADNESSRES")) {
			nextLine = skillReader.ReadLine ();
			stateResistances [9] = new DynamicDouble (@"Sadness", @"STN",
				Convert.ToDouble(nextLine.Substring(0, 6)));
			stateResistances [9].Holder = Convert.ToDouble (nextLine.Substring(7, 6));
			//Console.WriteLine (@"TEST: " + stateResistances[9].Name + " " + stateResistances[9].BaseValue + " " + stateResistances[9].Holder);
		}

		if (properties.Contains (@"FURYRES")) {
			nextLine = skillReader.ReadLine ();
			stateResistances [10] = new DynamicDouble (@"Fury", @"STN",
				Convert.ToDouble(nextLine.Substring(0, 6)));
			stateResistances [10].Holder = Convert.ToDouble (nextLine.Substring(7, 6));
			//Console.WriteLine (@"TEST: " + stateResistances[10].Name + " " + stateResistances[10].BaseValue + " " + stateResistances[10].Holder);
		}

		if (properties.Contains (@"SLEEPRES")) {
			nextLine = skillReader.ReadLine ();
			stateResistances [11] = new DynamicDouble (@"Sleep", @"GND",
				Convert.ToDouble(nextLine.Substring(0, 6)));
			stateResistances [11].Holder = Convert.ToDouble (nextLine.Substring(7, 6));
			//Console.WriteLine (@"TEST: " + stateResistances[11].Name + " " + stateResistances[11].BaseValue + " " + stateResistances[11].Holder);
		}

		if (properties.Contains (@"ADLERES")) {
			nextLine = skillReader.ReadLine ();
			stateResistances [12] = new DynamicDouble (@"Adle", @"AIR",
				Convert.ToDouble(nextLine.Substring(0, 6)));
			stateResistances [12].Holder = Convert.ToDouble (nextLine.Substring(7, 6));
			//Console.WriteLine (@"TEST: " + stateResistances[12].Name + " " + stateResistances[12].BaseValue + " " + stateResistances[12].Holder);
		}

		if (properties.Contains (@"FREEZERES")) {
			nextLine = skillReader.ReadLine ();
			stateResistances [13] = new DynamicDouble (@"Freeze", @"STN",
				Convert.ToDouble(nextLine.Substring(0, 6)));
			stateResistances [13].Holder = Convert.ToDouble (nextLine.Substring(7, 6));
			//Console.WriteLine (@"TEST: " + stateResistances[13].Name + " " + stateResistances[13].BaseValue + " " + stateResistances[13].Holder);
		}

		if (properties.Contains (@"IMMUNERES")) {
			nextLine = skillReader.ReadLine ();
			stateResistances [14] = new DynamicDouble (@"Immune", @"STN",
				Convert.ToDouble(nextLine.Substring(0, 6)));
			stateResistances [14].Holder = Convert.ToDouble (nextLine.Substring(7, 6));
			//Console.WriteLine (@"TEST: " + stateResistances[14].Name + " " + stateResistances[14].BaseValue + " " + stateResistances[14].Holder);
		}

		if (properties.Contains (@"INVULNERABLERES")) {
			nextLine = skillReader.ReadLine ();
			stateResistances [15] = new DynamicDouble (@"Invulnerable", @"STN",
				Convert.ToDouble(nextLine.Substring(0, 6)));
			stateResistances [15].Holder = Convert.ToDouble (nextLine.Substring(7, 6));
			//Console.WriteLine (@"TEST: " + stateResistances[15].Name + " " + stateResistances[15].BaseValue + " " + stateResistances[15].Holder);
		}

		if (properties.Contains (@"BURNRES")) {
			nextLine = skillReader.ReadLine ();
			stateResistances [16] = new DynamicDouble (@"Burn", @"GND",
				Convert.ToDouble(nextLine.Substring(0, 6)));
			stateResistances [16].Holder = Convert.ToDouble (nextLine.Substring(7, 6));
			//Console.WriteLine (@"TEST: " + stateResistances[16].Name + " " + stateResistances[16].BaseValue + " " + stateResistances[16].Holder);
		}

		if (properties.Contains (@"LEARNRES")) {
			nextLine = skillReader.ReadLine ();
			stateResistances [17] = new DynamicDouble (@"Learn", @"AIR",
				Convert.ToDouble(nextLine.Substring(0, 6)));
			stateResistances [17].Holder = Convert.ToDouble (nextLine.Substring(7, 6));
			//Console.WriteLine (@"TEST: " + stateResistances[17].Name + " " + stateResistances[17].BaseValue + " " + stateResistances[17].Holder);
		}

		if (properties.Contains (@"INVISIBLERES")) {
			nextLine = skillReader.ReadLine ();
			stateResistances [18] = new DynamicDouble (@"Invisible", @"STN",
				Convert.ToDouble(nextLine.Substring(0, 6)));
			stateResistances [18].Holder = Convert.ToDouble (nextLine.Substring(7, 6));
			//Console.WriteLine (@"TEST: " + stateResistances[18].Name + " " + stateResistances[18].BaseValue + " " + stateResistances[18].Holder);
		}

		if (properties.Contains (@"LEECHRES")) {
			nextLine = skillReader.ReadLine ();
			stateResistances [19] = new DynamicDouble (@"Leech", @"STN",
				Convert.ToDouble(nextLine.Substring(0, 6)));
			stateResistances [19].Holder = Convert.ToDouble (nextLine.Substring(7, 6));
			//Console.WriteLine (@"TEST: " + stateResistances[19].Name + " " + stateResistances[19].BaseValue + " " + stateResistances[19].Holder);
		}

		ArrayList linkedSkills = new ArrayList();
		int linkSkills = NumberConverter.ConvertToInt (skillReader.ReadLine().Substring(0, 3));
		for (int i = 0; i < linkSkills; i++) {
			linkedSkills.Add (loadSkill (p, playerName, skillReader.ReadLine(), @"Player", null));
			//print (name + " " + i);
		}

		Skill cancel = loadSkill (p, @"Cancel", null);

		Skill counter = null;

		int timer = NumberConverter.ConvertToInt(skillReader.ReadLine().Substring(0, 3));
		Skill timed = null;

		Skill trap = null;

		ArrayList learn = new ArrayList();

		ArrayList forget = new ArrayList ();

		ArrayList whiff = new ArrayList ();

		Skill dropSkill = null;

		string[] inputs = new string[NumberConverter.ConvertToInt (skillReader.ReadLine().Substring(0, 3))];
		for (int inp = 0; inp < inputs.Length; inp++) {
			inputs [inp] = skillReader.ReadLine ();
		}

		Skill s = null;

		if (!airGrab) {
			s = new Skill (name, @"Grab", description, properties, type, conditions, phrases,
				          specificPhrases, range, speed, hitStun, motionRange, force, widthOrRadius, ratio, hitRate, reps, links, threshold, numUses,
				          recovery, cost, change, statAugments, stateAugments, extraStateAugments, elements, stateResistances,
				linkedSkills, counter, timed, timer, trap, dropSkill, learn, forget, whiff, inputs, null);
		} else {
			s = new Skill (name, @"AirGrab", description, properties, type, conditions, phrases,
				specificPhrases, range, speed, hitStun, motionRange, force, widthOrRadius, ratio, hitRate, reps, links, threshold, numUses,
				recovery, cost, change, statAugments, stateAugments, extraStateAugments, elements, stateResistances,
				linkedSkills, counter, timed, timer, trap, dropSkill, learn, forget, whiff, inputs, null);
		}

        /**
		if (properties.Contains(@"PCANCEL")) {
			s.CancelSkill = cancel;
		}
        */

		skillReader.BaseStream.Position = 0;
		skillReader.DiscardBufferedData ();
		skillReader.Close ();
		return s;
	}

	//LOAD SKILL
	public Skill loadSkill (Player p, string skillName, Skill mst)
	{
		//StreamReader skillReader = new StreamReader (@"Players/*Universal/Skills/" + skillName + ".txt");
		StreamReader skillReader = new StreamReader (@"Assets/Resources/Universal/Skills/" + skillName + ".txt");

		string nextLine = @"";

		string name = skillReader.ReadLine ();
		string description = skillReader.ReadLine();
		string properties = skillReader.ReadLine ();
		string type = skillReader.ReadLine ();

		string[] conditions = new string[NumberConverter.ConvertToInt (skillReader.ReadLine().Substring(0, 3))];
		for (int i = 0; i < conditions.Length; i++) {
			conditions [i] = skillReader.ReadLine ();
		}
		string[] phrases = new string[NumberConverter.ConvertToInt (skillReader.ReadLine().Substring(0, 3))];
		for (int i = 0; i < phrases.Length; i++) {
			phrases [i] = skillReader.ReadLine ();
		}

		string[][] specificPhrases = new string[NumberConverter.ConvertToInt (skillReader.ReadLine().Substring(0, 3))][];
		for (int i = 0; i < specificPhrases.Length; i++) {
			nextLine = skillReader.ReadLine ();
			specificPhrases[i] = new string[2];
			specificPhrases [i] [0] = nextLine.Substring(0, nextLine.IndexOf(':'));
			specificPhrases [i] [1] = nextLine.Substring (nextLine.IndexOf(':') + 1);
		}

		int range = NumberConverter.ConvertToInt (skillReader.ReadLine().Substring(0, 3));
		int speed = NumberConverter.ConvertToInt (skillReader.ReadLine().Substring(0, 3));
		int hitStun = NumberConverter.ConvertToInt (skillReader.ReadLine().Substring(0, 3));
		int motionRange = NumberConverter.ConvertToInt (skillReader.ReadLine().Substring(0, 3));

		nextLine = skillReader.ReadLine ();
		State force = new State(@"Force", @"FRC", NumberConverter.ConvertToInt(nextLine.Substring(0, 3)),
			0.0, 0, Convert.ToDouble(nextLine.Substring(4, 6)), false, @"is augmented!");
		int widthOrRadius = NumberConverter.ConvertToInt (skillReader.ReadLine().Substring(0, 3));
		Double ratio = Convert.ToDouble (skillReader.ReadLine().Substring(0, 6));
		Double hitRate = Convert.ToDouble (skillReader.ReadLine().Substring(0, 6));
		int reps = NumberConverter.ConvertToInt (skillReader.ReadLine().Substring(0, 3));

		Boolean[] links = new bool[NumberConverter.ConvertToInt(skillReader.ReadLine().Substring(0, 3))];
		for (int i = 0; i < links.Length; i++) {
			links [i] = skillReader.ReadLine ().ToLower().Equals (@"true");
		}

		int threshold = NumberConverter.ConvertToInt (skillReader.ReadLine().Substring(0, 3));
		int numUses = NumberConverter.ConvertToInt (skillReader.ReadLine().Substring(0, 3));
		int recovery = NumberConverter.ConvertToInt (skillReader.ReadLine().Substring(0, 3));

		int[] cost = new int[4];
		int[] change = new int[4];

		/**
			 *
			 * METER COSTS
			 *
			 */

		cost [0] = NumberConverter.ConvertToInt (skillReader.ReadLine().Substring(0, 6));
		cost [1] = NumberConverter.ConvertToInt (skillReader.ReadLine().Substring(0, 6));
		cost [2] = NumberConverter.ConvertToInt (skillReader.ReadLine().Substring(0, 6));
		cost [3] = NumberConverter.ConvertToInt (skillReader.ReadLine().Substring(0, 6));

		/**
			 *
			 * METER AUGMENTS
			 *
			 */

		change [0] = NumberConverter.ConvertToInt (skillReader.ReadLine().Substring(0, 6));
		change [1] = NumberConverter.ConvertToInt (skillReader.ReadLine().Substring(0, 6));
		change [2] = NumberConverter.ConvertToInt (skillReader.ReadLine().Substring(0, 6));
		change [3] = NumberConverter.ConvertToInt (skillReader.ReadLine().Substring(0, 6));

		/**
			 *
			 * STAT AUGMENTS
			 *
			 */

		State[] statAugments = new State[10];

		nextLine = skillReader.ReadLine ();
		statAugments [0] = new State (@"Strength", @"STR", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		statAugments [1] = new State (@"Grit", @"GRT", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		statAugments [2] = new State (@"Magick", @"MAG", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		statAugments [3] = new State (@"Resistance", @"RES", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		statAugments [4] = new State (@"Dexterity", @"DEX", 0, Convert.ToDouble(nextLine.Substring(0, 6)),
			NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		statAugments [5] = new State (@"Speed", @"SPD", 0, Convert.ToDouble(nextLine.Substring(0, 6)),
			NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		statAugments [6] = new State (@"Proration", @"PRO", 0, Convert.ToDouble(nextLine.Substring(0, 6)),
			NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		statAugments [7] = new State (@"Movement", @"MOV", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		statAugments [8] = new State (@"Teamwork", @"TWK", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		statAugments [9] = new State (@"Luck", @"LCK", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");


		/**
			 * 
			 * ELEMENTS
			 *
			 */

		State[] elements = new State[10];
		nextLine = skillReader.ReadLine ();
		elements [0] = new State (@"Fire", @"FIR", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		elements [1] = new State (@"Ice", @"ICE", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		elements [2] = new State (@"Electricity", @"ELE", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		elements [3] = new State (@"Wind", @"WIN", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		elements [4] = new State (@"Water", @"WAT", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		elements [5] = new State (@"Earth", @"EAR", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		elements [6] = new State (@"Metal", @"MET", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		elements [7] = new State (@"Darkness", @"DAR", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		elements [8] = new State (@"Light", @"LIG", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		elements [9] = new State (@"Break", @"BRK", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");


		/**
			 * 
			 * STATE AUGMENTS
			 * 
			 */

		State[] stateAugments = new State[20];

		nextLine = skillReader.ReadLine ();
		stateAugments [0] = new State (@"Stun Hit", @"STN", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		stateAugments [1] = new State (@"Grounded", @"GND", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		stateAugments [2] = new State (@"Airborne", @"AIR", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		stateAugments [3] = new State (@"Counter", @"CTR", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		stateAugments [4] = new State (@"Parry", @"PAR", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		stateAugments [5] = new State (@"Poison", @"PSN", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		stateAugments [6] = new State (@"Regen", @"REG", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		stateAugments [7] = new State (@"Daze", @"DZE", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		stateAugments [8] = new State (@"Confuse", @"CNF", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		stateAugments [9] = new State (@"Sadness", @"SAD", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		stateAugments [10] = new State (@"Fury", @"FUR", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		stateAugments [11] = new State (@"Sleep", @"SLP", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		stateAugments [12] = new State (@"Adle", @"ADL", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		stateAugments [13] = new State (@"Freeze", @"FRZ", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		stateAugments [14] = new State (@"Immune", @"IMM", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		stateAugments [15] = new State (@"Invulnerable", @"INV", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		stateAugments [16] = new State (@"Burn", @"BRN", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		stateAugments [17] = new State (@"Learn", @"LRN", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		stateAugments [18] = new State (@"Invisible", @"INI", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		stateAugments [19] = new State (@"Leech", @"LCH", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		//EXTRA STATES
		ArrayList extraStateAugments = new ArrayList ();
		if (type.Contains (@"BLIND ")) {
			extraStateAugments.Add (new State (@"Blind", @"BLD", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
				0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
				Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!"));
		}

		DynamicDouble[] stateResistances = new DynamicDouble [20];
		for (int i = 0; i < stateResistances.Length; i++) {
			stateResistances [i] = new DynamicDouble (@"N/A", @"N/A", 0.0);
		}

		if (properties.Contains (@"STUNRES")) {
			nextLine = skillReader.ReadLine ();
			stateResistances [0] = new DynamicDouble (@"Stun Hit", @"STN",
				Convert.ToDouble(nextLine.Substring(0, 6)));
			stateResistances [0].Holder = Convert.ToDouble (nextLine.Substring(7, 6));
			//Console.WriteLine (@"TEST: " + stateResistances[0].Name + " " + stateResistances[0].BaseValue + " " + stateResistances[0].Holder);
		}

		if (properties.Contains (@"GROUNDEDRES")) {
			nextLine = skillReader.ReadLine ();
			stateResistances [1] = new DynamicDouble (@"Grounded", @"GND",
				Convert.ToDouble(nextLine.Substring(0, 6)));
			stateResistances [1].Holder = Convert.ToDouble (nextLine.Substring(7, 6));
			//Console.WriteLine (@"TEST: " + stateResistances[1].Name + " " + stateResistances[1].BaseValue + " " + stateResistances[1].Holder);
		}

		if (properties.Contains (@"AIRBORNERES")) {
			nextLine = skillReader.ReadLine ();
			stateResistances [2] = new DynamicDouble (@"Airborne", @"AIR",
				Convert.ToDouble(nextLine.Substring(0, 6)));
			stateResistances [2].Holder = Convert.ToDouble (nextLine.Substring(7, 6));
			//Console.WriteLine (@"TEST: " + stateResistances[2].Name + " " + stateResistances[2].BaseValue + " " + stateResistances[2].Holder);
		}

		if (properties.Contains (@"COUNTERRES")) {
			nextLine = skillReader.ReadLine ();
			stateResistances [3] = new DynamicDouble (@"Counter", @"STN",
				Convert.ToDouble(nextLine.Substring(0, 6)));
			stateResistances [3].Holder = Convert.ToDouble (nextLine.Substring(7, 6));
			//Console.WriteLine (@"TEST: " + stateResistances[3].Name + " " + stateResistances[3].BaseValue + " " + stateResistances[3].Holder);
		}

		if (properties.Contains (@"PARRYRES")) {
			nextLine = skillReader.ReadLine ();
			stateResistances [4] = new DynamicDouble (@"Parry", @"STN",
				Convert.ToDouble(nextLine.Substring(0, 6)));
			stateResistances [4].Holder = Convert.ToDouble (nextLine.Substring(7, 6));
			//Console.WriteLine (@"TEST: " + stateResistances[4].Name + " " + stateResistances[4].BaseValue + " " + stateResistances[4].Holder);
		}

		if (properties.Contains (@"POISONRES")) {
			nextLine = skillReader.ReadLine ();
			stateResistances [5] = new DynamicDouble (@"Poison", @"STN",
				Convert.ToDouble(nextLine.Substring(0, 6)));
			stateResistances [5].Holder = Convert.ToDouble (nextLine.Substring(7, 6));
			//Console.WriteLine (@"TEST: " + stateResistances[5].Name + " " + stateResistances[5].BaseValue + " " + stateResistances[5].Holder);
		}

		if (properties.Contains (@"REGENRES")) {
			nextLine = skillReader.ReadLine ();
			stateResistances [6] = new DynamicDouble (@"Regen", @"GND",
				Convert.ToDouble(nextLine.Substring(0, 6)));
			stateResistances [6].Holder = Convert.ToDouble (nextLine.Substring(7, 6));
			//Console.WriteLine (@"TEST: " + stateResistances[6].Name + " " + stateResistances[6].BaseValue + " " + stateResistances[6].Holder);
		}

		if (properties.Contains (@"DAZERES")) {
			nextLine = skillReader.ReadLine ();
			stateResistances [7] = new DynamicDouble (@"Daze", @"AIR",
				Convert.ToDouble(nextLine.Substring(0, 6)));
			stateResistances [7].Holder = Convert.ToDouble (nextLine.Substring(7, 6));
			//Console.WriteLine (@"TEST: " + stateResistances[7].Name + " " + stateResistances[7].BaseValue + " " + stateResistances[7].Holder);
		}

		if (properties.Contains (@"CONFUSERES")) {
			nextLine = skillReader.ReadLine ();
			stateResistances [8] = new DynamicDouble (@"Confuse", @"STN",
				Convert.ToDouble(nextLine.Substring(0, 6)));
			stateResistances [8].Holder = Convert.ToDouble (nextLine.Substring(7, 6));
			//Console.WriteLine (@"TEST: " + stateResistances[8].Name + " " + stateResistances[8].BaseValue + " " + stateResistances[8].Holder);
		}

		if (properties.Contains (@"SADNESSRES")) {
			nextLine = skillReader.ReadLine ();
			stateResistances [9] = new DynamicDouble (@"Sadness", @"STN",
				Convert.ToDouble(nextLine.Substring(0, 6)));
			stateResistances [9].Holder = Convert.ToDouble (nextLine.Substring(7, 6));
			//Console.WriteLine (@"TEST: " + stateResistances[9].Name + " " + stateResistances[9].BaseValue + " " + stateResistances[9].Holder);
		}

		if (properties.Contains (@"FURYRES")) {
			nextLine = skillReader.ReadLine ();
			stateResistances [10] = new DynamicDouble (@"Fury", @"STN",
				Convert.ToDouble(nextLine.Substring(0, 6)));
			stateResistances [10].Holder = Convert.ToDouble (nextLine.Substring(7, 6));
			//Console.WriteLine (@"TEST: " + stateResistances[10].Name + " " + stateResistances[10].BaseValue + " " + stateResistances[10].Holder);
		}

		if (properties.Contains (@"SLEEPRES")) {
			nextLine = skillReader.ReadLine ();
			stateResistances [11] = new DynamicDouble (@"Sleep", @"GND",
				Convert.ToDouble(nextLine.Substring(0, 6)));
			stateResistances [11].Holder = Convert.ToDouble (nextLine.Substring(7, 6));
			//Console.WriteLine (@"TEST: " + stateResistances[11].Name + " " + stateResistances[11].BaseValue + " " + stateResistances[11].Holder);
		}

		if (properties.Contains (@"ADLERES")) {
			nextLine = skillReader.ReadLine ();
			stateResistances [12] = new DynamicDouble (@"Adle", @"AIR",
				Convert.ToDouble(nextLine.Substring(0, 6)));
			stateResistances [12].Holder = Convert.ToDouble (nextLine.Substring(7, 6));
			//Console.WriteLine (@"TEST: " + stateResistances[12].Name + " " + stateResistances[12].BaseValue + " " + stateResistances[12].Holder);
		}

		if (properties.Contains (@"FREEZERES")) {
			nextLine = skillReader.ReadLine ();
			stateResistances [13] = new DynamicDouble (@"Freeze", @"STN",
				Convert.ToDouble(nextLine.Substring(0, 6)));
			stateResistances [13].Holder = Convert.ToDouble (nextLine.Substring(7, 6));
			//Console.WriteLine (@"TEST: " + stateResistances[13].Name + " " + stateResistances[13].BaseValue + " " + stateResistances[13].Holder);
		}

		if (properties.Contains (@"IMMUNERES")) {
			nextLine = skillReader.ReadLine ();
			stateResistances [14] = new DynamicDouble (@"Immune", @"STN",
				Convert.ToDouble(nextLine.Substring(0, 6)));
			stateResistances [14].Holder = Convert.ToDouble (nextLine.Substring(7, 6));
			//Console.WriteLine (@"TEST: " + stateResistances[14].Name + " " + stateResistances[14].BaseValue + " " + stateResistances[14].Holder);
		}

		if (properties.Contains (@"INVULNERABLERES")) {
			nextLine = skillReader.ReadLine ();
			stateResistances [15] = new DynamicDouble (@"Invulnerable", @"STN",
				Convert.ToDouble(nextLine.Substring(0, 6)));
			stateResistances [15].Holder = Convert.ToDouble (nextLine.Substring(7, 6));
			//Console.WriteLine (@"TEST: " + stateResistances[15].Name + " " + stateResistances[15].BaseValue + " " + stateResistances[15].Holder);
		}

		if (properties.Contains (@"BURNRES")) {
			nextLine = skillReader.ReadLine ();
			stateResistances [16] = new DynamicDouble (@"Burn", @"GND",
				Convert.ToDouble(nextLine.Substring(0, 6)));
			stateResistances [16].Holder = Convert.ToDouble (nextLine.Substring(7, 6));
			//Console.WriteLine (@"TEST: " + stateResistances[16].Name + " " + stateResistances[16].BaseValue + " " + stateResistances[16].Holder);
		}

		if (properties.Contains (@"LEARNRES")) {
			nextLine = skillReader.ReadLine ();
			stateResistances [17] = new DynamicDouble (@"Learn", @"AIR",
				Convert.ToDouble(nextLine.Substring(0, 6)));
			stateResistances [17].Holder = Convert.ToDouble (nextLine.Substring(7, 6));
			//Console.WriteLine (@"TEST: " + stateResistances[17].Name + " " + stateResistances[17].BaseValue + " " + stateResistances[17].Holder);
		}

		if (properties.Contains (@"INVISIBLERES")) {
			nextLine = skillReader.ReadLine ();
			stateResistances [18] = new DynamicDouble (@"Invisible", @"STN",
				Convert.ToDouble(nextLine.Substring(0, 6)));
			stateResistances [18].Holder = Convert.ToDouble (nextLine.Substring(7, 6));
			//Console.WriteLine (@"TEST: " + stateResistances[18].Name + " " + stateResistances[18].BaseValue + " " + stateResistances[18].Holder);
		}

		if (properties.Contains (@"LEECHRES")) {
			nextLine = skillReader.ReadLine ();
			stateResistances [19] = new DynamicDouble (@"Leech", @"STN",
				Convert.ToDouble(nextLine.Substring(0, 6)));
			stateResistances [19].Holder = Convert.ToDouble (nextLine.Substring(7, 6));
			//Console.WriteLine (@"TEST: " + stateResistances[19].Name + " " + stateResistances[19].BaseValue + " " + stateResistances[19].Holder);
		}

		Skill s = new Skill (name, skillName, description, properties, type, conditions, phrases,
			specificPhrases, range, speed, hitStun, motionRange, force, widthOrRadius, ratio, hitRate, reps, links, threshold, numUses,
			recovery, cost, change, statAugments, stateAugments, extraStateAugments, elements, stateResistances,
			new ArrayList(), null, null, 0, null, null, new ArrayList (), new ArrayList (), new ArrayList (), null, mst);

		if (properties.Contains(@"TRNREM¢")) {
			s.TurnsRemaining = Convert.ToInt32(properties.Substring (properties.IndexOf ('¢') + 1, 3));
		}

		int linkSkills = NumberConverter.ConvertToInt (skillReader.ReadLine().Substring(0, 3));
		for (int i = 0; i < linkSkills; i++) {
			s.LinkSkill (loadSkill(p, skillReader.ReadLine(), s));
			//print (name + " " + i);
		}

        /**
		if (properties.Contains(@"PCANCEL")) {
            s.CancelSkill = loadSkill (p, @"Cancel", s);
        }
        */

        if (p != null) {
			if (properties.Contains (@"NORM ") || properties.Contains (@"NORM2 ") || properties.Contains (@"NORM3 ")) {
				p.normalAddedSkills.Add (s);
			}

			if (properties.Contains (@"SPEC ") || properties.Contains (@"SPEC2 ") || properties.Contains (@"SPEC3 ")) {
				p.specialAddedSkills.Add (s);
			}

			if (properties.Contains (@"VITA ") || properties.Contains (@"VITA2 ") || properties.Contains (@"VITA3 ")) {
				p.vitalityAddedSkills.Add (s);
			}

			if (properties.Contains (@"BRST ") || properties.Contains (@"BRST2 ") || properties.Contains (@"BRST3 ")) {
				p.burstAddedSkills.Add (s);
			}

			if (properties.Contains (@"SACT ") || properties.Contains (@"SACT2 ") || properties.Contains (@"SACT3 ")) {
				p.sactionAddedSkills.Add (s);
			}

			if (properties.Contains (@"JMPS ") || properties.Contains (@"JMPS2 ") || properties.Contains (@"JMPS3 ")) {
				p.singleAddedSkills.Add (s);
			}

			if (properties.Contains (@"ITMS ") || properties.Contains (@"ITMS2 ") || properties.Contains (@"ITMS3 ")) {
				p.singleAddedSkills.Add (s);
			}


			if (properties.Contains (@"SNGL ") || properties.Contains (@"SNGL2 ") || properties.Contains (@"SNGL3 ")) {
				p.singleAddedSkills.Add (s);
			}
		}

		Skill counter = null;
		if (stateAugments [3].Potency != 0) {
			s.CounterSkill = loadSkill (p, skillReader.ReadLine(), s);
		}

		s.Timer = NumberConverter.ConvertToInt(skillReader.ReadLine().Substring(0, 3));
		if (s.Timer != 0 && (!properties.Contains(@"TIMESTOP ") || type.Contains (@"TIME"))) {
			s.TimerSkill = loadSkill (p, skillReader.ReadLine(), s);
		}

		if (type.Contains (@"OBJECT ")) {
			s.TrapSkill = loadSkill (p, skillReader.ReadLine(), s);
		}

		if (type.Contains (@"ADDSKL ")) {
			int lrn = NumberConverter.ConvertToInt(skillReader.ReadLine().Substring(0, 3));

			for (int i = 0; i < lrn; i ++)  {
				s.LearnSkills.Add (loadSkill (p, skillReader.ReadLine(), s));
			}
		}

		if (type.Contains (@"FORGETSKL ")) {
			int forg = NumberConverter.ConvertToInt(skillReader.ReadLine().Substring(0, 3));

			for (int i = 0; i < forg; i ++)  {
				s.ForgetSkills.Add (skillReader.ReadLine());
			}
		}

		if (properties.Contains (@"DROP")) {
			s.DropItem = loadSkill (p, skillReader.ReadLine(), s);
		}

		if (type.Contains (@"WHFSKL")) {
			int whf = NumberConverter.ConvertToInt(skillReader.ReadLine().Substring(0, 3));

			for (int i = 0; i < whf; i ++)  {
				s.WhiffSkills.Add (loadSkill (p, skillReader.ReadLine(), s));
			}
		}

		string inputReader = skillReader.ReadLine ();

		s.Inputs = new string[NumberConverter.ConvertToInt (inputReader.Substring(0, 3))];
		for (int inp = 0; inp < s.Inputs.Length; inp++) {
			s.Inputs [inp] = skillReader.ReadLine ();
		}

		s.MasterSkill = mst;
		if (s.MasterSkill != null) {
			properties += @"CHILD ";
		} else {
			properties += @"MASTER ";
		}

		skillReader.BaseStream.Position = 0;
		skillReader.DiscardBufferedData ();
		skillReader.Close ();
		return s;
	}

	/**
		 * 
		 * 		private string name;
				private string searchName;
				private Skill connectedSkill;
				private Location location;
				private Map objectMap;
				private int strength;
				private int height;
				private int remainingTime;
		 */

	public MapObject loadObject (string objectName, Player p)
	{
		//StreamReader objectReader = new StreamReader (@"Players/*Universal/Objects/" + objectName + ".txt");
		StreamReader objectReader = new StreamReader (@"Assets/Resources/Universal/Objects/" + objectName + ".txt");
		string name = objectReader.ReadLine ();

		string skillName = objectReader.ReadLine ();
		Skill connectedSkill = null;
		if (!skillName.Equals (@"null")) {
			connectedSkill = loadSkill (p, objectReader.ReadLine (), null);
			if (p != null) {
				p.learn (connectedSkill, false, connectedSkill.TurnsRemaining);
			}
		}


		int strength = NumberConverter.ConvertToInt(objectReader.ReadLine ().Substring(0, 3));
		int height = NumberConverter.ConvertToInt(objectReader.ReadLine ().Substring(0, 3));
		int remainingTime = NumberConverter.ConvertToInt(objectReader.ReadLine ().Substring(0, 3));
		string conditions = objectReader.ReadLine ();

		objectReader.BaseStream.Position = 0;
		objectReader.DiscardBufferedData ();
		objectReader.Close ();

		return new MapObject (name, objectName, connectedSkill, new Location(-1, -1), null,
			strength, height, remainingTime, conditions);
	}

	public Item loadItem (string playerName, ArrayList noAmmoList, string itemName)
	{
		//StreamReader itemReader = new StreamReader (@"Players/" + playerName + "/Items/" + itemName + ".txt");
		StreamReader itemReader = new StreamReader (@"Assets/Resources/Players/" + playerName + @"/Items/" + itemName + ".txt");
		//string nextLine = @"";

		string name = itemReader.ReadLine ();
		string specialConditions = itemReader.ReadLine ();
		string searchName = itemName;
		string description = itemReader.ReadLine ();
		string areaOfEffect = itemReader.ReadLine ();

		double[] statAugments = new double[10];
		for (int i = 0; i < statAugments.Length; i++) {
			statAugments [i] = Convert.ToDouble (itemReader.ReadLine().Substring(0, 6));
			//Console.WriteLine (name + " " + statAugments[i]);
		}

		int[] stateAugments = new int[19];
		for (int i = 0; i < stateAugments.Length; i++) {
			stateAugments [i] = NumberConverter.ConvertToInt (itemReader.ReadLine().Substring(0, 6));
		}

		int[] elementAugments = new int[10];
		for (int i = 0; i < statAugments.Length; i++) {
			elementAugments [i] = NumberConverter.ConvertToInt (itemReader.ReadLine().Substring(0, 6));
		}

		int[] resistances = new int[10];
		for (int i = 0; i < resistances.Length; i++) {
			resistances [i] = NumberConverter.ConvertToInt(itemReader.ReadLine().Substring(0, 6));
		}

		int maxUses = NumberConverter.ConvertToInt (itemReader.ReadLine().Substring(0, 6));

		int numUses;
		if (noAmmoList.Contains (itemName)) {
			numUses = 0;
		} else {
			numUses = maxUses;
		}

		Boolean canDuplicate = itemReader.ReadLine ().Contains (@"true");


		Item it = new Item (name, specialConditions, searchName, description, areaOfEffect, statAugments, stateAugments,
			elementAugments, resistances, numUses, canDuplicate);

		it.NumUses = numUses;
		it.MaxUses = maxUses;

		itemReader.BaseStream.Position = 0;
		itemReader.DiscardBufferedData ();
		itemReader.Close ();
		return it;
	}

	public State loadState (string sName, double probability)
	{
		StreamReader stateReader = new StreamReader (@"Assets/Resources/Universal/States/" + sName + ".txt");
		string name = stateReader.ReadLine ();
		string abbreviation = stateReader.ReadLine ();
		Boolean malicious = stateReader.ReadLine ().Equals (@"true");
		string phrase = stateReader.ReadLine ();

		stateReader.Close ();

		return new State (name, abbreviation, 0, 0.0, 0, probability, malicious, phrase);
	}

	//LOAD SKILL
	public Skill loadSkill (Player p, string playerName, string skillName, string tpe, Skill mst)
	{
		StreamReader skillReader = null;
        StreamReader animationReader = null;
        if (!p.TacticalMode) {
            /**
            animationReader = new StreamReader(@"Assets/Resources/Players/" + playerName + "@/Skills/" + skillName);
            animationReader.Close();
            */
        }

        /**
		if (skillName.Equals (@"Engage")) {
			return loadSkill (null, @"Custom", null);
		}
		*/

		if (tpe.Equals (@"Player")) {
            
			//skillReader = new StreamReader (@"Players/" + playerName + "/Skills/" + skillName + ".txt");

			skillReader = new StreamReader (@"Assets/Resources/Players/" + playerName + @"/Skills/" + skillName + ".txt");
		} else {
			//skillReader = new StreamReader (@"Maps/" + playerName + "/" + playerName + "/Skills/" + skillName + ".txt");
			skillReader = new StreamReader (@"Assets/Resources/Maps/" + playerName + @"/" + playerName + @"/Skills/" + skillName + ".txt");
		}

		string nextLine = @"";

		string name = skillReader.ReadLine ();
		string description = skillReader.ReadLine();
		string properties = skillReader.ReadLine ();
		string type = skillReader.ReadLine ();

		string[] conditions = new string[NumberConverter.ConvertToInt (skillReader.ReadLine().Substring(0, 3))];
		for (int i = 0; i < conditions.Length; i++) {
			conditions [i] = skillReader.ReadLine ();
		}
		string[] phrases = new string[NumberConverter.ConvertToInt (skillReader.ReadLine().Substring(0, 3))];
		//Console.WriteLine (skillName + " " + phrases.Length);
		for (int i = 0; i < phrases.Length; i++) {
			phrases [i] = skillReader.ReadLine ();
		}

		//string[][] specificPhrases = new string[NumberConverter.ConvertToInt (skillReader.ReadLine().Substring(0, 3))][];
		Int16 length;
		Int16.TryParse (skillReader.ReadLine().Substring(0, 3), out length);
		string[][] specificPhrases = new string[length][];
		//Console.WriteLine (skillName + " " + specificPhrases.Length);
		for (int i = 0; i < specificPhrases.Length; i++) {
			nextLine = skillReader.ReadLine ();
			specificPhrases[i] = new string[2];

			//if (nextLine.IndexOf (':') < 0) {
			//	throw new NullReferenceException (skillName + " is a problem");
			//}

			specificPhrases [i] [0] = nextLine.Substring(0, nextLine.IndexOf(':'));
			specificPhrases [i] [1] = nextLine.Substring (nextLine.IndexOf(':') + 1);
		}

		int range = NumberConverter.ConvertToInt (skillReader.ReadLine().Substring(0, 3));
		int speed = NumberConverter.ConvertToInt (skillReader.ReadLine().Substring(0, 3));
		int hitStun = NumberConverter.ConvertToInt (skillReader.ReadLine().Substring(0, 3));
		int motionRange = NumberConverter.ConvertToInt (skillReader.ReadLine().Substring(0, 3));

		nextLine = skillReader.ReadLine ();
		State force = new State(@"Force", @"FRC", NumberConverter.ConvertToInt(nextLine.Substring(0, 3)),

			0.0, 0, Convert.ToDouble(nextLine.Substring(4, 6)), false, @"is augmented!");
		int widthOrRadius = NumberConverter.ConvertToInt (skillReader.ReadLine().Substring(0, 3));
		Double ratio = Convert.ToDouble (skillReader.ReadLine().Substring(0, 6));
		Double hitRate = Convert.ToDouble (skillReader.ReadLine().Substring(0, 6));
		int reps = NumberConverter.ConvertToInt (skillReader.ReadLine().Substring(0, 3));

		Boolean[] links = new bool[NumberConverter.ConvertToInt(skillReader.ReadLine().Substring(0, 3))];
		for (int i = 0; i < links.Length; i++) {
			if (links.Length != reps) {
				throw new NullReferenceException (name);
			}
			links [i] = skillReader.ReadLine ().ToLower().Equals (@"true");
		}

		int threshold = NumberConverter.ConvertToInt (skillReader.ReadLine().Substring(0, 3));
		int numUses = NumberConverter.ConvertToInt (skillReader.ReadLine().Substring(0, 3));
		int recovery = NumberConverter.ConvertToInt (skillReader.ReadLine().Substring(0, 3));

		int[] cost = new int[4];
		int[] change = new int[4];

		/**
			 *
			 * METER COSTS
			 *
			 */

		cost [0] = NumberConverter.ConvertToInt (skillReader.ReadLine().Substring(0, 6));
		cost [1] = NumberConverter.ConvertToInt (skillReader.ReadLine().Substring(0, 6));
		cost [2] = NumberConverter.ConvertToInt (skillReader.ReadLine().Substring(0, 6));
		cost [3] = NumberConverter.ConvertToInt (skillReader.ReadLine().Substring(0, 6));

		/**
			 *
			 * METER AUGMENTS
			 *
			 */

		change [0] = NumberConverter.ConvertToInt (skillReader.ReadLine().Substring(0, 6));
		change [1] = NumberConverter.ConvertToInt (skillReader.ReadLine().Substring(0, 6));
		change [2] = NumberConverter.ConvertToInt (skillReader.ReadLine().Substring(0, 6));
		change [3] = NumberConverter.ConvertToInt (skillReader.ReadLine().Substring(0, 6));

		/**
			 *
			 * STAT AUGMENTS
			 *
			 */

		State[] statAugments = new State[10];

		nextLine = skillReader.ReadLine ();
		statAugments [0] = new State (@"Strength", @"STR", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		statAugments [1] = new State (@"Grit", @"GRT", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		statAugments [2] = new State (@"Magick", @"MAG", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		statAugments [3] = new State (@"Resistance", @"RES", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		statAugments [4] = new State (@"Dexterity", @"DEX", 0, Convert.ToDouble(nextLine.Substring(0, 6)),
			NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		statAugments [5] = new State (@"Speed", @"SPD", 0, Convert.ToDouble(nextLine.Substring(0, 6)),
			NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		statAugments [6] = new State (@"Proration", @"PRO", 0, Convert.ToDouble(nextLine.Substring(0, 6)),
			NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		statAugments [7] = new State (@"Movement", @"MOV", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		statAugments [8] = new State (@"Teamwork", @"TWK", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		statAugments [9] = new State (@"Luck", @"LCK", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");


		/**
			 * 
			 * ELEMENTS
			 *
			 */

		State[] elements = new State[10];
		nextLine = skillReader.ReadLine ();
		elements [0] = new State (@"Fire", @"FIR", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		elements [1] = new State (@"Ice", @"ICE", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		elements [2] = new State (@"Electricity", @"ELE", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		elements [3] = new State (@"Wind", @"WIN", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		elements [4] = new State (@"Water", @"WAT", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		elements [5] = new State (@"Earth", @"EAR", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		elements [6] = new State (@"Metal", @"MET", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		elements [7] = new State (@"Darkness", @"DAR", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		elements [8] = new State (@"Light", @"LIG", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		elements [9] = new State (@"Break", @"BRK", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");


		/**
			 * 
			 * STATE AUGMENTS
			 * 
			 */

		State[] stateAugments = new State[20];

		nextLine = skillReader.ReadLine ();
		stateAugments [0] = new State (@"Stun Hit", @"STN", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		stateAugments [1] = new State (@"Grounded", @"GND", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		stateAugments [2] = new State (@"Airborne", @"AIR", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		stateAugments [3] = new State (@"Counter", @"CTR", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		stateAugments [4] = new State (@"Parry", @"PAR", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		stateAugments [5] = new State (@"Poison", @"PSN", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		stateAugments [6] = new State (@"Regen", @"REG", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		stateAugments [7] = new State (@"Daze", @"DZE", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		stateAugments [8] = new State (@"Confuse", @"CNF", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		stateAugments [9] = new State (@"Sadness", @"SAD", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		stateAugments [10] = new State (@"Fury", @"FUR", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		stateAugments [11] = new State (@"Sleep", @"SLP", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		stateAugments [12] = new State (@"Adle", @"ADL", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		stateAugments [13] = new State (@"Freeze", @"FRZ", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		stateAugments [14] = new State (@"Immune", @"IMM", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		stateAugments [15] = new State (@"Invulnerable", @"INV", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		stateAugments [16] = new State (@"Burn", @"BRN", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		stateAugments [17] = new State (@"Learn", @"LRN", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		stateAugments [18] = new State (@"Invisible", @"INI", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");

		nextLine = skillReader.ReadLine ();
		stateAugments [19] = new State (@"Leech", @"LCH", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
			0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
			Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!");


		//EXTRA STATES
		ArrayList extraStateAugments = new ArrayList ();
		if (type.Contains (@"BLIND ")) {
			extraStateAugments.Add (new State (@"Blind", @"BLD", NumberConverter.ConvertToInt(nextLine.Substring(0, 6)),
				0.0, NumberConverter.ConvertToInt(nextLine.Substring(7, 6)),
				Convert.ToDouble(nextLine.Substring(14, 6)), false, @"is augmented!"));
		}

		DynamicDouble[] stateResistances = new DynamicDouble [20];
		for (int i = 0; i < stateResistances.Length; i++) {
			stateResistances [i] = new DynamicDouble (@"N/A", @"N/A", 0.0);
		}

		if (properties.Contains (@"STUNRES")) {
			nextLine = skillReader.ReadLine ();
			stateResistances [0] = new DynamicDouble (@"Stun Hit", @"STN",
				Convert.ToDouble(nextLine.Substring(0, 6)));
			stateResistances [0].Holder = Convert.ToDouble (nextLine.Substring(7, 6));
			//Console.WriteLine (@"TEST: " + stateResistances[0].Name + " " + stateResistances[0].BaseValue + " " + stateResistances[0].Holder);
		}

		if (properties.Contains (@"GROUNDEDRES")) {
			nextLine = skillReader.ReadLine ();
			stateResistances [1] = new DynamicDouble (@"Grounded", @"GND",
				Convert.ToDouble(nextLine.Substring(0, 6)));
			stateResistances [1].Holder = Convert.ToDouble (nextLine.Substring(7, 6));
			//Console.WriteLine (@"TEST: " + stateResistances[1].Name + " " + stateResistances[1].BaseValue + " " + stateResistances[1].Holder);
		}

		if (properties.Contains (@"AIRBORNERES")) {
			nextLine = skillReader.ReadLine ();
			stateResistances [2] = new DynamicDouble (@"Airborne", @"AIR",
				Convert.ToDouble(nextLine.Substring(0, 6)));
			stateResistances [2].Holder = Convert.ToDouble (nextLine.Substring(7, 6));
			//Console.WriteLine (@"TEST: " + stateResistances[2].Name + " " + stateResistances[2].BaseValue + " " + stateResistances[2].Holder);
		}

		if (properties.Contains (@"COUNTERRES")) {
			nextLine = skillReader.ReadLine ();
			stateResistances [3] = new DynamicDouble (@"Counter", @"STN",
				Convert.ToDouble(nextLine.Substring(0, 6)));
			stateResistances [3].Holder = Convert.ToDouble (nextLine.Substring(7, 6));
			//Console.WriteLine (@"TEST: " + stateResistances[3].Name + " " + stateResistances[3].BaseValue + " " + stateResistances[3].Holder);
		}

		if (properties.Contains (@"PARRYRES")) {
			nextLine = skillReader.ReadLine ();
			stateResistances [4] = new DynamicDouble (@"Parry", @"STN",
				Convert.ToDouble(nextLine.Substring(0, 6)));
			stateResistances [4].Holder = Convert.ToDouble (nextLine.Substring(7, 6));
			//Console.WriteLine (@"TEST: " + stateResistances[4].Name + " " + stateResistances[4].BaseValue + " " + stateResistances[4].Holder);
		}

		if (properties.Contains (@"POISONRES")) {
			nextLine = skillReader.ReadLine ();
			stateResistances [5] = new DynamicDouble (@"Poison", @"STN",
				Convert.ToDouble(nextLine.Substring(0, 6)));
			stateResistances [5].Holder = Convert.ToDouble (nextLine.Substring(7, 6));
			//Console.WriteLine (@"TEST: " + stateResistances[5].Name + " " + stateResistances[5].BaseValue + " " + stateResistances[5].Holder);
		}

		if (properties.Contains (@"REGENRES")) {
			nextLine = skillReader.ReadLine ();
			stateResistances [6] = new DynamicDouble (@"Regen", @"GND",
				Convert.ToDouble(nextLine.Substring(0, 6)));
			stateResistances [6].Holder = Convert.ToDouble (nextLine.Substring(7, 6));
			//Console.WriteLine (@"TEST: " + stateResistances[6].Name + " " + stateResistances[6].BaseValue + " " + stateResistances[6].Holder);
		}

		if (properties.Contains (@"DAZERES")) {
			nextLine = skillReader.ReadLine ();
			stateResistances [7] = new DynamicDouble (@"Daze", @"AIR",
				Convert.ToDouble(nextLine.Substring(0, 6)));
			stateResistances [7].Holder = Convert.ToDouble (nextLine.Substring(7, 6));
			//Console.WriteLine (@"TEST: " + stateResistances[7].Name + " " + stateResistances[7].BaseValue + " " + stateResistances[7].Holder);
		}

		if (properties.Contains (@"CONFUSERES")) {
			nextLine = skillReader.ReadLine ();
			stateResistances [8] = new DynamicDouble (@"Confuse", @"STN",
				Convert.ToDouble(nextLine.Substring(0, 6)));
			stateResistances [8].Holder = Convert.ToDouble (nextLine.Substring(7, 6));
			//Console.WriteLine (@"TEST: " + stateResistances[8].Name + " " + stateResistances[8].BaseValue + " " + stateResistances[8].Holder);
		}

		if (properties.Contains (@"SADNESSRES")) {
			nextLine = skillReader.ReadLine ();
			stateResistances [9] = new DynamicDouble (@"Sadness", @"STN",
				Convert.ToDouble(nextLine.Substring(0, 6)));
			stateResistances [9].Holder = Convert.ToDouble (nextLine.Substring(7, 6));
			//Console.WriteLine (@"TEST: " + stateResistances[9].Name + " " + stateResistances[9].BaseValue + " " + stateResistances[9].Holder);
		}

		if (properties.Contains (@"FURYRES")) {
			nextLine = skillReader.ReadLine ();
			stateResistances [10] = new DynamicDouble (@"Fury", @"STN",
				Convert.ToDouble(nextLine.Substring(0, 6)));
			stateResistances [10].Holder = Convert.ToDouble (nextLine.Substring(7, 6));
			//Console.WriteLine (@"TEST: " + stateResistances[10].Name + " " + stateResistances[10].BaseValue + " " + stateResistances[10].Holder);
		}

		if (properties.Contains (@"SLEEPRES")) {
			nextLine = skillReader.ReadLine ();
			stateResistances [11] = new DynamicDouble (@"Sleep", @"GND",
				Convert.ToDouble(nextLine.Substring(0, 6)));
			stateResistances [11].Holder = Convert.ToDouble (nextLine.Substring(7, 6));
			//Console.WriteLine (@"TEST: " + stateResistances[11].Name + " " + stateResistances[11].BaseValue + " " + stateResistances[11].Holder);
		}

		if (properties.Contains (@"ADLERES")) {
			nextLine = skillReader.ReadLine ();
			stateResistances [12] = new DynamicDouble (@"Adle", @"AIR",
				Convert.ToDouble(nextLine.Substring(0, 6)));
			stateResistances [12].Holder = Convert.ToDouble (nextLine.Substring(7, 6));
			//Console.WriteLine (@"TEST: " + stateResistances[12].Name + " " + stateResistances[12].BaseValue + " " + stateResistances[12].Holder);
		}

		if (properties.Contains (@"FREEZERES")) {
			nextLine = skillReader.ReadLine ();
			stateResistances [13] = new DynamicDouble (@"Freeze", @"STN",
				Convert.ToDouble(nextLine.Substring(0, 6)));
			stateResistances [13].Holder = Convert.ToDouble (nextLine.Substring(7, 6));
			//Console.WriteLine (@"TEST: " + stateResistances[13].Name + " " + stateResistances[13].BaseValue + " " + stateResistances[13].Holder);
		}

		if (properties.Contains (@"IMMUNERES")) {
			nextLine = skillReader.ReadLine ();
			stateResistances [14] = new DynamicDouble (@"Immune", @"STN",
				Convert.ToDouble(nextLine.Substring(0, 6)));
			stateResistances [14].Holder = Convert.ToDouble (nextLine.Substring(7, 6));
			//Console.WriteLine (@"TEST: " + stateResistances[14].Name + " " + stateResistances[14].BaseValue + " " + stateResistances[14].Holder);
		}

		if (properties.Contains (@"INVULNERABLERES")) {
			nextLine = skillReader.ReadLine ();
			stateResistances [15] = new DynamicDouble (@"Invulnerable", @"STN",
				Convert.ToDouble(nextLine.Substring(0, 6)));
			stateResistances [15].Holder = Convert.ToDouble (nextLine.Substring(7, 6));
			//Console.WriteLine (@"TEST: " + stateResistances[15].Name + " " + stateResistances[15].BaseValue + " " + stateResistances[15].Holder);
		}

		if (properties.Contains (@"BURNRES")) {
			nextLine = skillReader.ReadLine ();
			stateResistances [16] = new DynamicDouble (@"Burn", @"GND",
				Convert.ToDouble(nextLine.Substring(0, 6)));
			stateResistances [16].Holder = Convert.ToDouble (nextLine.Substring(7, 6));
			//Console.WriteLine (@"TEST: " + stateResistances[16].Name + " " + stateResistances[16].BaseValue + " " + stateResistances[16].Holder);
		}

		if (properties.Contains (@"LEARNRES")) {
			nextLine = skillReader.ReadLine ();
			stateResistances [17] = new DynamicDouble (@"Learn", @"AIR",
				Convert.ToDouble(nextLine.Substring(0, 6)));
			stateResistances [17].Holder = Convert.ToDouble (nextLine.Substring(7, 6));
			//Console.WriteLine (@"TEST: " + stateResistances[17].Name + " " + stateResistances[17].BaseValue + " " + stateResistances[17].Holder);
		}

		if (properties.Contains (@"INVISIBLERES")) {
			nextLine = skillReader.ReadLine ();
			stateResistances [18] = new DynamicDouble (@"Invisible", @"STN",
				Convert.ToDouble(nextLine.Substring(0, 6)));
			stateResistances [18].Holder = Convert.ToDouble (nextLine.Substring(7, 6));
			//Console.WriteLine (@"TEST: " + stateResistances[18].Name + " " + stateResistances[18].BaseValue + " " + stateResistances[18].Holder);
		}

		if (properties.Contains (@"LEECHRES")) {
			nextLine = skillReader.ReadLine ();
			stateResistances [19] = new DynamicDouble (@"Leech", @"STN",
				Convert.ToDouble(nextLine.Substring(0, 6)));
			stateResistances [19].Holder = Convert.ToDouble (nextLine.Substring(7, 6));
			//Console.WriteLine (@"TEST: " + stateResistances[19].Name + " " + stateResistances[19].BaseValue + " " + stateResistances[19].Holder);
		}

		Skill s = new Skill (name, skillName, description, properties, type, conditions, phrases,
			specificPhrases, range, speed, hitStun, motionRange, force, widthOrRadius, ratio, hitRate, reps, links, threshold, numUses,
			recovery, cost, change, statAugments, stateAugments, extraStateAugments, elements, stateResistances,
			new ArrayList (), null, null, 0, null, null, new ArrayList (), new ArrayList (), new ArrayList (), null, mst);
		

		int linkSkills = NumberConverter.ConvertToInt (skillReader.ReadLine().Substring(0, 3));
		for (int i = 0; i < linkSkills; i++) {
			s.LinkSkill (loadSkill(p, playerName, skillReader.ReadLine(), tpe, s));
			//print (name + " " + i);
		}


		if (properties.Contains(@"TRNREM¢")) {
			s.TurnsRemaining = Convert.ToInt32(properties.Substring (properties.IndexOf ('¢') + 1, 3));
		}

        /**
		if (properties.Contains(@"PCANCEL")) {
            s.CancelSkill = loadSkill (p, @"Cancel", s);
        }
        */

        if (properties.Contains (@"PLF1")) {
			s.LinkSkill (loadSkill (p, @"PLF1", s));
		}

		if (properties.Contains (@"PLF2")) {
			s.LinkSkill (loadSkill (p, @"PLF2", s));
		}

		if (properties.Contains (@"PLF3")) {
			s.LinkSkill (loadSkill (p, @"PLF3", s));
		}


		if (p != null) {
			if (properties.Contains (@"NORM ") || properties.Contains (@"NORM2 ") || properties.Contains (@"NORM3 ")) {
				p.normalAddedSkills.Add (s);
			}

			if (properties.Contains (@"SPEC ") || properties.Contains (@"SPEC2 ") || properties.Contains (@"SPEC3 ")) {
				p.specialAddedSkills.Add (s);
			}

			if (properties.Contains (@"VITA ") || properties.Contains (@"VITA2 ") || properties.Contains (@"VITA3 ")) {
				p.vitalityAddedSkills.Add (s);
			}

			if (properties.Contains (@"BRST ") || properties.Contains (@"BRST2 ") || properties.Contains (@"BRST3 ")) {
				p.burstAddedSkills.Add (s);
			}

			if (properties.Contains (@"SACT ") || properties.Contains (@"SACT2 ") || properties.Contains (@"SACT3 ")) {
				p.sactionAddedSkills.Add (s);
			}

			if (properties.Contains (@"SNGL ") || properties.Contains (@"SNGL2 ") || properties.Contains (@"SNGL3 ")) {
				p.singleAddedSkills.Add (s);
			}
		}



		if (stateAugments [3].Potency != 0) {
			s.CounterSkill = loadSkill (p, playerName, skillReader.ReadLine(), tpe, s);
		}

		s.Timer = NumberConverter.ConvertToInt(skillReader.ReadLine().Substring(0, 3));
		if (s.Timer != 0 && (!properties.Contains(@"TIMESTOP ") || type.Contains (@"TIME"))) {
			s.TimerSkill = loadSkill (p, playerName, skillReader.ReadLine(), tpe, s);
		}

		if (type.Contains (@"OBJECT ")) {
			s.TrapSkill = loadSkill (p, playerName, skillReader.ReadLine(), tpe, s);
		}

		if (type.Contains (@"ADDSKL ")) {
			int lrn = NumberConverter.ConvertToInt(skillReader.ReadLine().Substring(0, 3));

			for (int i = 0; i < lrn; i ++)  {
				s.LearnSkills.Add (loadSkill (p, playerName, skillReader.ReadLine(), tpe, s));
			}
		}

		if (type.Contains (@"FORGETSKL ")) {
			int forg = NumberConverter.ConvertToInt(skillReader.ReadLine().Substring(0, 3));

			for (int i = 0; i < forg; i ++)  {
				s.ForgetSkills.Add (skillReader.ReadLine());
			}
		}

		if (properties.Contains (@"DROP")) {
			s.DropItem = loadSkill (p, playerName, skillReader.ReadLine(), tpe, s);
		}

		if (type.Contains (@"WHFSKL")) {
			int whf = NumberConverter.ConvertToInt(skillReader.ReadLine().Substring(0, 3));

			for (int i = 0; i < whf; i ++)  {
				s.WhiffSkills.Add (loadSkill (p, playerName, skillReader.ReadLine(), tpe, s));
			}
		}

		string inputReader = skillReader.ReadLine ();

		if (inputReader == null || inputReader.Substring (0, 3) == null) {
			throw new NullReferenceException (s.Name);
		}

		s.Inputs = new string[NumberConverter.ConvertToInt (inputReader.Substring(0, 3))];
		for (int inp = 0; inp < s.Inputs.Length; inp++) {
			s.Inputs [inp] = skillReader.ReadLine ();
		}

		skillReader.BaseStream.Position = 0;
		skillReader.DiscardBufferedData ();
		skillReader.Close ();
		return s;
	}

    public Player loadSavedPlayer (string name, string path, Boolean tactical)
    {
        StreamReader currentReader = new StreamReader (path);
        Player player = loadPlayer (name, @"Player", true, true, tactical);

        player = loadPlayer (name, @"Player", false, true, tactical);
        ///player.clearSkillsAndItems ();

        ///player.CurrentLocation = new Location(NumberConverter.ConvertToInt(row), NumberConverter.ConvertToInt(column));

        string lineToAdd = "", nextLine = "";
        //NAME
        currentReader.ReadLine ();

        //AGE
        lineToAdd += currentReader.ReadLine() + '\n';

        //SEX
        lineToAdd += currentReader.ReadLine() + '\n';

        //NATIONALITY
        lineToAdd += currentReader.ReadLine() + '\n';

        //SPECIES
        lineToAdd += currentReader.ReadLine() + '\n';

        //WEIGHT
        lineToAdd += currentReader.ReadLine() + '\n';

        //HEIGHT
        lineToAdd += currentReader.ReadLine() + '\n';

        //HANDEDNESS
        lineToAdd += currentReader.ReadLine() + '\n';

        //STYLE
        nextLine = currentReader.ReadLine();

        //RATIO
        nextLine = currentReader.ReadLine();
        player.Ratio = NumberConverter.ConvertToInt(nextLine.Substring(nextLine.IndexOf(':') + 2));

        //POWER LEVEL
        lineToAdd += currentReader.ReadLine() + '\n';

        //CLASS
        lineToAdd += currentReader.ReadLine() + '\n';

        //PLAY STYLE
        nextLine = currentReader.ReadLine();
        player.Experience = NumberConverter.ConvertToInt(nextLine.Substring(nextLine.IndexOf('-') - 1, 1));

        //TEAM
        lineToAdd += currentReader.ReadLine() + '\n';

        //TIME REMAINING
        nextLine = currentReader.ReadLine();
        player.TimeRemaining = NumberConverter.ConvertToInt(nextLine.Substring(nextLine.IndexOf(':') + 2));

        //LEVEL
        nextLine = currentReader.ReadLine();
        player.Level = NumberConverter.ConvertToInt(nextLine.Substring(nextLine.IndexOf(':') + 1));

        //EXPERIENCE
        nextLine = currentReader.ReadLine ();
        player.Experience = NumberConverter.ConvertToInt(nextLine.Substring(nextLine.IndexOf(':') + 1));

        //HEALTH
        nextLine = currentReader.ReadLine ();
        //throw new NullReferenceException (nextLine.Substring (nextLine.IndexOf("/") + 1));
        player.Health.MeterLevel = NumberConverter.ConvertToInt (nextLine.Substring (0, nextLine.IndexOf ("/")));
        player.Health.MeterMax = NumberConverter.ConvertToInt (nextLine.Substring (nextLine.IndexOf ("/") + 1, nextLine.IndexOf (':') - nextLine.IndexOf ("/") + 1));
        player.Health.MeterShift = NumberConverter.ConvertToInt (nextLine.Substring(nextLine.IndexOf (':') + 1));

        //RUSH
        nextLine = currentReader.ReadLine ();
        player.Rush.MeterLevel = NumberConverter.ConvertToInt(nextLine.Substring(0, nextLine.IndexOf("/")));
        player.Rush.MeterMax = NumberConverter.ConvertToInt(nextLine.Substring(nextLine.IndexOf("/") + 1, nextLine.IndexOf(':') - nextLine.IndexOf("/") + 1));
        player.Rush.MeterShift = NumberConverter.ConvertToInt(nextLine.Substring(nextLine.IndexOf(':') + 1));

        //GUARD
        nextLine = currentReader.ReadLine();
        player.Guard.MeterLevel = NumberConverter.ConvertToInt(nextLine.Substring(0, nextLine.IndexOf("/")));
        player.Guard.MeterMax = NumberConverter.ConvertToInt(nextLine.Substring(nextLine.IndexOf("/") + 1, nextLine.IndexOf(':') - nextLine.IndexOf("/") + 1));
        player.Guard.MeterShift = NumberConverter.ConvertToInt(nextLine.Substring(nextLine.IndexOf(':') + 1));

        //VITALITY
        nextLine = currentReader.ReadLine();
        player.Vitality.MeterLevel = NumberConverter.ConvertToInt(nextLine.Substring(0, nextLine.IndexOf("/")));
        player.Vitality.MeterMax = NumberConverter.ConvertToInt(nextLine.Substring(nextLine.IndexOf("/") + 1, nextLine.IndexOf(':') - nextLine.IndexOf("/") + 1));
        player.Vitality.MeterShift = NumberConverter.ConvertToInt(nextLine.Substring(nextLine.IndexOf(':') + 1));

        //STUN
        nextLine = currentReader.ReadLine();
        player.Stun.MeterLevel = NumberConverter.ConvertToInt(nextLine.Substring(0, nextLine.IndexOf("/")));
        player.Stun.MeterMax = NumberConverter.ConvertToInt(nextLine.Substring(nextLine.IndexOf("/") + 1, nextLine.IndexOf(':') - nextLine.IndexOf("/") + 1));
        player.Stun.MeterShift = NumberConverter.ConvertToInt(nextLine.Substring(nextLine.IndexOf(':') + 1));

        int index;
        //STRENGTH
        nextLine = currentReader.ReadLine ();
        player.Strength = NumberConverter.ConvertToInt (nextLine.Substring (nextLine.IndexOf (':') + 1));
        nextLine = currentReader.ReadLine ();
        index = NumberConverter.ConvertToInt(nextLine.Substring(nextLine.IndexOf (':') + 1));
        for (int i = 0; i < index; i++)
        {
            nextLine = currentReader.ReadLine ();
            player.strength.addVariant (new State (@"", @"",
                                                  NumberConverter.ConvertToInt (nextLine.Substring (0, nextLine.IndexOf (','))), 0,
                                                  NumberConverter.ConvertToInt (nextLine.Substring (nextLine.IndexOf (',') + 1)),
                                                  0, false, @""));
        }

        //GRIT
        nextLine = currentReader.ReadLine ();
        player.Grit = NumberConverter.ConvertToInt(nextLine.Substring(nextLine.IndexOf(':') + 1));
        nextLine = currentReader.ReadLine ();
        index = NumberConverter.ConvertToInt (nextLine.Substring(nextLine.IndexOf(':') + 1));
        for (int i = 0; i < index; i++)
        {
            nextLine = currentReader.ReadLine();
            player.grit.addVariant(new State(@"", @"",
                                                  NumberConverter.ConvertToInt(nextLine.Substring(0, nextLine.IndexOf(','))), 0,
                                                  NumberConverter.ConvertToInt(nextLine.Substring(nextLine.IndexOf(',') + 1)),
                                                  0, false, @""));
        }

        //MAGICK
        nextLine = currentReader.ReadLine();
        player.Magick = NumberConverter.ConvertToInt(nextLine.Substring(nextLine.IndexOf(':') + 1));
        nextLine = currentReader.ReadLine();
        index = NumberConverter.ConvertToInt(nextLine.Substring(nextLine.IndexOf(':') + 1));
        for (int i = 0; i < index; i++)
        {
            nextLine = currentReader.ReadLine ();
            player.magick.addVariant(new State(@"", @"",
                                                  NumberConverter.ConvertToInt(nextLine.Substring (0, nextLine.IndexOf (','))), 0,
                                                  NumberConverter.ConvertToInt(nextLine.Substring (nextLine.IndexOf (',') + 1)),
                                                  0, false, @""));
        }

        //RESISTANCE
        nextLine = currentReader.ReadLine ();
        player.Resistance = NumberConverter.ConvertToInt(nextLine.Substring(nextLine.IndexOf (':') + 1));
        nextLine = currentReader.ReadLine ();
        index = NumberConverter.ConvertToInt (nextLine.Substring(nextLine.IndexOf(':') + 1));
        for (int i = 0; i < index; i++)
        {
            nextLine = currentReader.ReadLine();
            player.resistance.addVariant(new State(@"", @"",
                                                  NumberConverter.ConvertToInt(nextLine.Substring(0, nextLine.IndexOf(','))), 0,
                                                  NumberConverter.ConvertToInt(nextLine.Substring(nextLine.IndexOf(',') + 1)),
                                                  0, false, @""));
        }

        //SPEED
        nextLine = currentReader.ReadLine();
        player.Speed = NumberConverter.ConvertToDouble(nextLine.Substring(nextLine.IndexOf(':') + 1));
        nextLine = currentReader.ReadLine();
        index = NumberConverter.ConvertToInt(nextLine.Substring(nextLine.IndexOf(':') + 1));
        for (int i = 0; i < index; i++)
        {
            nextLine = currentReader.ReadLine();
            player.speed.addVariant(new State(@"", @"",
                                              0, NumberConverter.ConvertToDouble(nextLine.Substring(0, nextLine.IndexOf(','))),
                                                  NumberConverter.ConvertToInt(nextLine.Substring(nextLine.IndexOf(',') + 1)),
                                                  0, false, @""));
        }

        //DEXTERITY
        nextLine = currentReader.ReadLine();
        player.Dexterity = NumberConverter.ConvertToDouble(nextLine.Substring(nextLine.IndexOf(':') + 1));
        nextLine = currentReader.ReadLine();
        index = NumberConverter.ConvertToInt(nextLine.Substring(nextLine.IndexOf(':') + 1));
        for (int i = 0; i < index; i++)
        {
            nextLine = currentReader.ReadLine();
            player.strength.addVariant(new State(@"", @"",
                                                  0, NumberConverter.ConvertToDouble (nextLine.Substring(0, nextLine.IndexOf(','))),
                                                  NumberConverter.ConvertToInt(nextLine.Substring(nextLine.IndexOf(',') + 1)),
                                                  0, false, @""));
        }

        //PRORATION
        nextLine = currentReader.ReadLine();
        player.Proration = (float)NumberConverter.ConvertToDouble ( nextLine.Substring(nextLine.IndexOf(':') + 1));
        nextLine = currentReader.ReadLine();
        index = NumberConverter.ConvertToInt(nextLine.Substring(nextLine.IndexOf(':') + 1));
        for (int i = 0; i < index; i++)
        {
            nextLine = currentReader.ReadLine();
            player.strength.addVariant(new State(@"", @"",
                                                  0, NumberConverter.ConvertToInt(nextLine.Substring(0, nextLine.IndexOf(','))),
                                                  NumberConverter.ConvertToInt(nextLine.Substring(nextLine.IndexOf(',') + 1)),
                                                  0, false, @""));
        }

        //MOVEMENT
        nextLine = currentReader.ReadLine();
        player.Movement = NumberConverter.ConvertToInt(nextLine.Substring(nextLine.IndexOf(':') + 1));
        nextLine = currentReader.ReadLine();
        index = NumberConverter.ConvertToInt(nextLine.Substring(nextLine.IndexOf(':') + 1));
        for (int i = 0; i < index; i++)
        {
            nextLine = currentReader.ReadLine();
            player.movement.addVariant(new State(@"", @"",
                                                  NumberConverter.ConvertToInt(nextLine.Substring(0, nextLine.IndexOf(','))), 0,
                                                  NumberConverter.ConvertToInt(nextLine.Substring(nextLine.IndexOf(',') + 1)),
                                                  0, false, @""));
        }

        //TEAMWORK
        nextLine = currentReader.ReadLine();
        player.Teamwork = NumberConverter.ConvertToInt(nextLine.Substring(nextLine.IndexOf(':') + 1));
        nextLine = currentReader.ReadLine();
        index = NumberConverter.ConvertToInt(nextLine.Substring(nextLine.IndexOf(':') + 1));
        for (int i = 0; i < index; i++)
        {
            nextLine = currentReader.ReadLine();
            player.teamwork.addVariant(new State(@"", @"",
                                                  NumberConverter.ConvertToInt(nextLine.Substring(0, nextLine.IndexOf(','))), 0,
                                                  NumberConverter.ConvertToInt(nextLine.Substring(nextLine.IndexOf(',') + 1)),
                                                  0, false, @""));
        }

        //LUCK
        nextLine = currentReader.ReadLine();
        player.Luck = NumberConverter.ConvertToInt(nextLine.Substring(nextLine.IndexOf(':') + 1));
        nextLine = currentReader.ReadLine();
        index = NumberConverter.ConvertToInt(nextLine.Substring(nextLine.IndexOf(':') + 1));
        for (int i = 0; i < index; i++)
        {
            nextLine = currentReader.ReadLine();
            player.luck.addVariant(new State(@"", @"",
                                                  NumberConverter.ConvertToInt(nextLine.Substring(0, nextLine.IndexOf(','))), 0,
                                                  NumberConverter.ConvertToInt(nextLine.Substring(nextLine.IndexOf(',') + 1)),
                                                  0, false, @""));
        }

        //ACTION POINTS
        lineToAdd += currentReader.ReadLine() + '\n';

        //MOVES REMAINING
        nextLine = currentReader.ReadLine();
        player.MovesRemaining = NumberConverter.ConvertToInt(nextLine.Substring(nextLine.IndexOf(':') + 1));

        ///STATES
        for (int i = 0; i < player.States.Length; i ++) {
            currentReader.ReadLine();
            nextLine = currentReader.ReadLine ();
            player.States[i].Add (new State (@"", @"",
                                             NumberConverter.ConvertToInt (nextLine.Substring (0, nextLine.IndexOf("/"))),
                                             NumberConverter.ConvertToInt (nextLine.Substring (nextLine.IndexOf ("/") + 1, nextLine.IndexOf('#'))),
                                             NumberConverter.ConvertToInt (nextLine.Substring (nextLine.IndexOf('#') + 1, nextLine.IndexOf(':'))),
                                             NumberConverter.ConvertToInt (nextLine.Substring (0, nextLine.IndexOf(':') + 1)),
                                             player.States[i].Malicious,
                                            @""));
        }

        ///STATE RESISTANCE
        for (int i = 0; i < player.StateResistances.Length; i++)
        {
            nextLine = currentReader.ReadLine();
            nextLine = currentReader.ReadLine();
            index = NumberConverter.ConvertToInt(nextLine.Substring(nextLine.IndexOf(':') + 1));
            for (int j = 0; j < index; j++)
            {
                nextLine = currentReader.ReadLine();
                player.StateResistances [j].addVariant (new State(@"", @"",
                                                      NumberConverter.ConvertToInt(nextLine.Substring(0, nextLine.IndexOf(','))), 0,
                                                      NumberConverter.ConvertToInt(nextLine.Substring(nextLine.IndexOf(',') + 1)),
                                                      0, false, @""));
            }
        }

        //FIRE

        lineToAdd += currentReader.ReadLine() + " ELEMENTS START HERE" + '\n';

        //ICE
        lineToAdd += currentReader.ReadLine() + '\n';

        //ELECTRICITY
        lineToAdd += currentReader.ReadLine() + '\n';

        //WIND
        lineToAdd += currentReader.ReadLine() + '\n';

        //WATER
        lineToAdd += currentReader.ReadLine() + '\n';

        //EARTH
        lineToAdd += currentReader.ReadLine() + '\n';

        //METAL
        lineToAdd += currentReader.ReadLine() + '\n';

        //DARK
        lineToAdd += currentReader.ReadLine() + '\n';

        //LIGHT
        lineToAdd += currentReader.ReadLine() + '\n';

        //CRUSH
        lineToAdd += currentReader.ReadLine() + '\n';

        //CONDITIONS
        string reps = currentReader.ReadLine();
        lineToAdd += reps + " IS IT, MAKE NO MISTAKE" + '\n';

        int count = NumberConverter.ConvertToInt(reps.Substring(reps.IndexOf(':') + 1));
        for (int k = 0; k < count; k++)
        {
            lineToAdd += currentReader.ReadLine() + '\n';
        }

        //TRACK SKILLS
        reps = currentReader.ReadLine();
        lineToAdd += reps + " WORKS HERE" + '\n';

        count = NumberConverter.ConvertToInt(reps.Substring(0, reps.IndexOf(':')));

        player.LoadedInfo = count + " TRACKING SKILLS" + '\n';

        for (int k = 0; k < count; k++)
        {
            player.LoadedInfo += currentReader.ReadLine();
        }

        //TIME ACTIVATED SKILLS
        reps = currentReader.ReadLine();
        lineToAdd += reps + " TIME HAS COME TODAY!" + '\n';

        if (reps == null || reps.IndexOf (':') == null) {
            throw new NullReferenceException (reps + @" PROBLEM " + lineToAdd);
        }

        count = NumberConverter.ConvertToInt(reps.Substring (0, reps.IndexOf (':')));

        player.LoadedInfo = count + " TIME ACTIVE SKILLS" + '\n';

        for (int k = 0; k < count; k++)
        {
            player.LoadedInfo += currentReader.ReadLine();
        }

        //DIRECTION
        reps = currentReader.ReadLine();
        player.Direction = reps.Substring(reps.IndexOf(':') + 1);
        lineToAdd += reps + " directs" + '\n';

        //OBJECT INDEX
        reps = currentReader.ReadLine();
        lineToAdd += reps + '\n';
        player.ObjectIndex = NumberConverter.ConvertToInt(reps.Substring(reps.IndexOf(':') + 1));

        //TURN ENDED
        reps = currentReader.ReadLine();
        lineToAdd += reps + '\n';
        player.TurnEnded = reps.Substring(reps.IndexOf(':') + 1).Equals("True");

        //COOLDOWN
        reps = currentReader.ReadLine();
        lineToAdd += reps + '\n';
        player.Cooldown = NumberConverter.ConvertToInt(reps.Substring(reps.IndexOf(':') + 1));

        //SPECIAL STATE
        reps = currentReader.ReadLine();
        lineToAdd += reps + " SPEC STATE" + '\n';
        player.SpecialState = reps.Substring(reps.IndexOf(':') + 1).Equals("True");

        //AIR OFF
        reps = currentReader.ReadLine();
        lineToAdd += reps + " AIR OFF" + '\n';
        player.AirOff = reps.Substring(reps.IndexOf(':') + 1).Equals("True");

        //HIT STUN ADJUSTMENT
        reps = currentReader.ReadLine();
        lineToAdd += reps + '\n';
        player.HitStunAdjustment = NumberConverter.ConvertToInt(reps.Substring(reps.IndexOf(':') + 1));

        //NUM RESTS
        reps = currentReader.ReadLine();
        lineToAdd += reps + '\n';
        player.NumRests = NumberConverter.ConvertToInt(reps.Substring(reps.IndexOf(':') + 1));

        //SKILL EXECUTED
        reps = currentReader.ReadLine();
        lineToAdd += reps + '\n';
        player.SkillExecuted = reps.Substring(reps.IndexOf(':') + 1).Equals("True");

        //HIGHEST COMBO
        reps = currentReader.ReadLine();
        lineToAdd += reps + '\n';
        player.HighestCombo = NumberConverter.ConvertToInt(reps.Substring(reps.IndexOf(':') + 1));

        //COMBO
        reps = currentReader.ReadLine();
        lineToAdd += reps + '\n';
        player.Combo = NumberConverter.ConvertToInt(reps.Substring(reps.IndexOf(':') + 1));

        //COMBO DAMAGE PER ROUND
        reps = currentReader.ReadLine();
        lineToAdd += reps + '\n';
        player.ComboDamagePerRound = NumberConverter.ConvertToInt(reps.Substring(reps.IndexOf(':') + 1));

        //FATIGUE
        reps = currentReader.ReadLine();
        lineToAdd += reps + '\n';
        player.Fatigue = NumberConverter.ConvertToInt(reps.Substring(reps.IndexOf(':') + 1));

        //RESIDUAL FATIGUE
        reps = currentReader.ReadLine();
        lineToAdd += reps + " Residual Fatigue" + '\n';
        player.ResidualFatigue = NumberConverter.ConvertToInt(reps.Substring(reps.IndexOf(':') + 1));

        //CURRENT PRORATION
        reps = currentReader.ReadLine();
        lineToAdd += reps + '\n';
        // throw new NullReferenceException (reps + " thought yo ass was legit?!" + lineToAdd);
        player.CurrentProration = Convert.ToDouble(reps.Substring(reps.IndexOf(':') + 1));

        //CURRENT HIT RATE
        reps = currentReader.ReadLine();
        player.CurrentHitRate = Convert.ToDouble(reps.Substring(reps.IndexOf(':') + 1));

        //SACTION ENABLED
        reps = currentReader.ReadLine();
        player.SActionEnabled = reps.Substring(reps.IndexOf(':') + 1).Equals("True");

        //REVERSE NAME
        reps = currentReader.ReadLine();
        player.ReverseName = reps.Substring(reps.IndexOf(':') + 1).Equals("True");

        //MAP SIZE
        reps = currentReader.ReadLine();
        player.MapSize = NumberConverter.ConvertToInt(reps.Substring(reps.IndexOf(':') + 1));

        //FIRST TURN
        reps = currentReader.ReadLine();
        player.FirstTurn = reps.Substring(reps.IndexOf(':') + 1).Equals("True");

        //NO MOTION
        reps = currentReader.ReadLine();
        player.NoMotion = reps.Substring(reps.IndexOf(':') + 1).Equals("True");

        //HURT STUN
        reps = currentReader.ReadLine();
        player.HurtStun = NumberConverter.ConvertToInt(reps.Substring(reps.IndexOf(':') + 1));

        //HAS CHANGED LOCATIONS - BOOL
        reps = currentReader.ReadLine();
        player.HasChangedLocations = reps.Substring(reps.IndexOf(':') + 1).Equals("True");

        //HAS CHANGED HEIGHTS - BOOL
        reps = currentReader.ReadLine();
        player.HasChangedHeights = reps.Substring(reps.IndexOf(':') + 1).Equals("True");

        //CRITICAL LIMIT - INT
        reps = currentReader.ReadLine();
        player.CriticalLimit = NumberConverter.ConvertToInt(reps.Substring(reps.IndexOf(':') + 1));

        //IS GUARDING - BOOL
        reps = currentReader.ReadLine();
        player.IsGuarding = reps.Substring(reps.IndexOf(':') + 1).Equals("True");

        //AUTOGUARD
        reps = currentReader.ReadLine();
        player.AutoGuard = reps.Substring(reps.IndexOf(':') + 1).Equals("True");

        //IS DIZZIED
        reps = currentReader.ReadLine();
        player.IsDizzied = reps.Substring(reps.IndexOf(':') + 1).Equals("True");

        //CAN ACT
        reps = currentReader.ReadLine();
        player.CanAct = reps.Substring(reps.IndexOf(':') + 1).Equals("True");

        //HAS ACTED
        reps = currentReader.ReadLine();
        player.HasActed = reps.Substring(reps.IndexOf(':') + 1).Equals("True");

        //HAS JUMPED
        reps = currentReader.ReadLine();
        player.HasJumped = reps.Substring(reps.IndexOf(':') + 1).Equals("True");

        //WAS DIZZIED
        reps = currentReader.ReadLine();
        player.WasDizzied = reps.Substring(reps.IndexOf(':') + 1).Equals("True");

        //WAS CRITICAL
        reps = currentReader.ReadLine();
        player.WasCritical = reps.Substring(reps.IndexOf(':') + 1).Equals("True");

        //IS RESTING
        reps = currentReader.ReadLine();
        player.IsResting = reps.Substring(reps.IndexOf(':') + 1).Equals("True");

        //IS TAUNTING
        reps = currentReader.ReadLine();
        player.IsTaunting = reps.Substring(reps.IndexOf(':') + 1).Equals("True");

        //TAUNT DECAY
        reps = currentReader.ReadLine();
        player.TauntDecay = NumberConverter.ConvertToInt(reps.Substring(reps.IndexOf(':') + 1));

        //WAS HIT
        reps = currentReader.ReadLine();
        player.WasHit = reps.Substring(reps.IndexOf(':') + 1).Equals("True");

        //IN RECOVERY
        reps = currentReader.ReadLine();
        player.InRecovery = reps.Substring(reps.IndexOf(':') + 1).Equals("True");

        //HAS FALLEN
        reps = currentReader.ReadLine();
        player.HasFallen = reps.Substring(reps.IndexOf(':') + 1).Equals("True");

        //CAN REST
        reps = currentReader.ReadLine();
        player.CanRest = reps.Substring(reps.IndexOf(':') + 1).Equals("True");

        //CAN BE REVIVED
        reps = currentReader.ReadLine();
        player.CanBeRevived = reps.Substring(reps.IndexOf(':') + 1).Equals("True");

        //CRITICAL ACTIVE
        reps = currentReader.ReadLine();
        player.CriticalActive = reps.Substring(reps.IndexOf(':') + 1).Equals("True");

        //BONUS ROUND ACTIVATED
        reps = currentReader.ReadLine();
        player.BonusRoundActivated = reps.Substring(reps.IndexOf(':') + 1).Equals("True");

        //HIT MALICIOUSLY
        reps = currentReader.ReadLine();
        player.HitMaliciously = reps.Substring(reps.IndexOf(':') + 1).Equals("True");

        //UNDEAD
        reps = currentReader.ReadLine();
        player.Undead = reps.Substring(reps.IndexOf(':') + 1).Equals("True");

        //HALF COSTS
        reps = currentReader.ReadLine();
        player.HalfCost = reps.Substring(reps.IndexOf(':') + 1).Equals("True");

        //NO COST
        reps = currentReader.ReadLine();
        player.NoCost = reps.Substring(reps.IndexOf(':') + 1).Equals("True");

        //LEECH
        reps = currentReader.ReadLine();
        player.Leech = reps.Substring(reps.IndexOf(':') + 1).Equals("True");

        //COUNTER STATE
        reps = currentReader.ReadLine();
        player.CounterState = reps.Substring(reps.IndexOf(':') + 1).Equals("True");

        //PREVIOUS TILE HEIGHT
        reps = currentReader.ReadLine();
        player.PreviousTileHeight = NumberConverter.ConvertToInt(reps.Substring(reps.IndexOf(':') + 1));

        //DAMAGE PER ROUND
        reps = currentReader.ReadLine();
        player.DamagePerRound = NumberConverter.ConvertToInt(reps.Substring(reps.IndexOf(':') + 1));

        //DAMAGE TOTAL
        reps = currentReader.ReadLine();
        player.DamageTotal = NumberConverter.ConvertToInt(reps.Substring(reps.IndexOf(':') + 1));

        //CONNECTED PER ROUND
        reps = currentReader.ReadLine();
        player.ConnectsPerRound = NumberConverter.ConvertToInt(reps.Substring(reps.IndexOf(':') + 1));

        //ATTACKS PER ROUND
        reps = currentReader.ReadLine();
        player.AttacksPerRound = NumberConverter.ConvertToInt(reps.Substring(reps.IndexOf(':') + 1));

        //CONNECTS TOTAL
        reps = currentReader.ReadLine();
        player.ConnectsTotal = NumberConverter.ConvertToInt(reps.Substring(reps.IndexOf(':') + 1));

        //ATTACKS TOTAL
        reps = currentReader.ReadLine();
        player.AttacksTotal = NumberConverter.ConvertToInt(reps.Substring(reps.IndexOf(':') + 1));

        //HEALING PER ROUND
        reps = currentReader.ReadLine();
        player.HealingPerRound = NumberConverter.ConvertToInt(reps.Substring(reps.IndexOf(':') + 1));

        //HEALING TOTAL
        reps = currentReader.ReadLine();
        player.HealingTotal = NumberConverter.ConvertToInt(reps.Substring(reps.IndexOf(':') + 1));

        //DAMAGE TAKEN PER ROUND
        reps = currentReader.ReadLine();
        player.DamageTakenPerRound = NumberConverter.ConvertToInt(reps.Substring(reps.IndexOf(':') + 1));

        //DAMAGE TAKEN TOTAL
        reps = currentReader.ReadLine();
        player.DamageTakenTotal = NumberConverter.ConvertToInt(reps.Substring(reps.IndexOf(':') + 1));

        //STATUS AFFLICTIONS TOTAL
        reps = currentReader.ReadLine();
        player.StatusAfflictionsTotal = NumberConverter.ConvertToInt(reps.Substring(reps.IndexOf(':') + 1));

        //STATUS AFFLICTIONS PER ROUND
        reps = currentReader.ReadLine();
        player.StatusAfflictionsPerRound = NumberConverter.ConvertToInt(reps.Substring(reps.IndexOf(':') + 1));

        //STATUS AIDS TOTAL
        reps = currentReader.ReadLine();
        player.StatusAidsTotal = NumberConverter.ConvertToInt(reps.Substring(reps.IndexOf(':') + 1));

        //STATUS AIDS PER ROUND
        reps = currentReader.ReadLine();
        player.StatusAidsPerRound = NumberConverter.ConvertToInt(reps.Substring(reps.IndexOf(':') + 1));

        //STUN PER ROUND
        reps = currentReader.ReadLine();
        player.StunPerRound = NumberConverter.ConvertToInt(reps.Substring(reps.IndexOf(':') + 1));

        //STUN TOTAL
        reps = currentReader.ReadLine();
        player.StunTotal = NumberConverter.ConvertToInt(reps.Substring(reps.IndexOf(':') + 1));

        //BONUS TIME
        reps = currentReader.ReadLine();
        player.BonusTime = NumberConverter.ConvertToInt(reps.Substring(reps.IndexOf(':') + 1));

        //FALLING HEIGHT
        reps = currentReader.ReadLine();
        player.FallingHeight = NumberConverter.ConvertToInt(reps.Substring(reps.IndexOf(':') + 1));

        //LANDING HEIGHT
        reps = currentReader.ReadLine();
        player.LandingHeight = NumberConverter.ConvertToInt(reps.Substring(reps.IndexOf(':') + 1));

        //GUARD STUN
        reps = currentReader.ReadLine();
        player.GuardStun = NumberConverter.ConvertToInt(reps.Substring(reps.IndexOf(':') + 1));

        //IN PLAY
        reps = currentReader.ReadLine();
        player.InPlay = reps.Substring(reps.IndexOf(':') + 1).Equals("True");

        //ON FIELD
        reps = currentReader.ReadLine();
        player.OnField = reps.Substring(reps.IndexOf(':') + 1).Equals("True");

        //LAST STAND
        reps = currentReader.ReadLine();
        player.LastStand = reps.Substring(reps.IndexOf(':') + 1).Equals("True");

        //WEAK FRAME
        reps = currentReader.ReadLine();
        player.WeakFrame = reps.Substring(reps.IndexOf(':') + 1).Equals("True");

        //VM GAIN BLOCKED
        reps = currentReader.ReadLine();
        player.VMGainBlocked = reps.Substring(reps.IndexOf(':') + 1).Equals("True");

        //BLOOD PRICE
        reps = currentReader.ReadLine();
        player.BloodPrice = reps.Substring(reps.IndexOf(':') + 1).Equals("True");

        //FLIGHT
        reps = currentReader.ReadLine();
        player.Flight = reps.Substring(reps.IndexOf(':') + 1).Equals("True");

        //BERSERK
        reps = currentReader.ReadLine();
        player.Berserk = reps.Substring(reps.IndexOf(':') + 1).Equals("True");

        //TIME STOPPED
        reps = currentReader.ReadLine();
        player.TimeStopped = reps.Substring(reps.IndexOf(':') + 1).Equals("True");

        //NO GUARD
        reps = currentReader.ReadLine();
        player.NoGuard = reps.Substring(reps.IndexOf(':') + 1).Equals("True");

        //NO CONDITIONS
        reps = currentReader.ReadLine();
        player.NoConditions = reps.Substring(reps.IndexOf(':') + 1).Equals("True");

        //CAN FLY
        reps = currentReader.ReadLine();
        player.CanFly = reps.Substring(reps.IndexOf(':') + 1).Equals("True");

        //ACTIVE PLAYER
        reps = currentReader.ReadLine();
        player.ActivePlayer = reps.Substring(reps.IndexOf(':') + 1).Equals("True");

        //HAS ACTED IN ROUND
        reps = currentReader.ReadLine();
        player.HasActedInRound = reps.Substring(reps.IndexOf(':') + 1).Equals("True");

        //IS CROUCHING
        reps = currentReader.ReadLine();
        player.IsCrouching = reps.Substring(reps.IndexOf(':') + 1).Equals("True");

        //CHAMPION
        reps = currentReader.ReadLine();
        player.Champion = reps.Substring(reps.IndexOf(':') + 1).Equals("True");

        //STAGGERED
        reps = currentReader.ReadLine();
        player.Staggered = reps.Substring(reps.IndexOf(':') + 1).Equals("True");

        if (player.Staggered)
        {

            //DIRECTION
            reps = currentReader.ReadLine();
            player.StaggerDirection = reps.Substring(reps.IndexOf(':') + 1);

            //TIME
            reps = currentReader.ReadLine();
            player.StaggerTime = NumberConverter.ConvertToInt(reps.Substring(reps.IndexOf(':') + 1));
        }

        //ASSIMILATED CLASS
        reps = currentReader.ReadLine();
        player.AssimilatedClass = reps.Substring(reps.IndexOf(':') + 1);

        if (player.AssimilatedClass != "")
        {

            //TIME
            reps = currentReader.ReadLine();
            player.AssimilatedTime = NumberConverter.ConvertToInt(reps.Substring(reps.IndexOf(':') + 1));
        }

        //INVENTORY
        reps = currentReader.ReadLine();
        count = NumberConverter.ConvertToInt(reps.Substring(reps.IndexOf(':') + 1));
        player.LoadedInfo = count + " INVENTORY" + '\n';
        for (int j = 0; j < count; j++)
        {
            player.LoadedInfo += currentReader.ReadLine();
        }

        currentReader.ReadLine();

        //SKILLS
        reps = currentReader.ReadLine();
        count = NumberConverter.ConvertToInt(reps.Substring(reps.IndexOf(':'), 1));
        player.LoadedInfo = count + " SKILLS" + '\n';
        for (int k = 0; k < count; k++)
        {
            player.LoadedInfo += currentReader.ReadLine();
        }

        //MASTER
        player.LoadedInfo += currentReader.ReadLine();
        //METER GAIN
        reps = currentReader.ReadLine();
        player.NoGainForMaster = reps.Substring(reps.IndexOf(':') + 1).Equals("True");

        //STORED PLAYER
        player.LoadedInfo += currentReader.ReadLine ();
        currentReader.Close(); 

        return player;
    }

    public Map loadMatch ()
    {
        Map mp = null;
        StreamReader mapWriter = new StreamReader(@"Assets/Resources/Data/SaveData/Slot1/Map.txt"),
        currentReader = null;

        Locatable currentObj;
        Player player;
        MapObject obj;
        Skill skill;
        ArrayList currentObjects = new ArrayList ();
        int numObjects;

        string lineToAdd;

        ArrayList roster = new ArrayList();
        ArrayList interactables = new ArrayList ();

        //mapWriter.WriteLine(mp.ToString());
        string nextLine;
        string row, column, type, name;
        //NAME
        nextLine = mapWriter.ReadLine ();
        //SIZE
        nextLine = mapWriter.ReadLine ();
        //SEARCH NAME
        name = mapWriter.ReadLine ();
        type = mapWriter.ReadLine ();

        mp = loadMap (name.Substring (name.IndexOf (':') + 1),
                      type.Substring (name.IndexOf (':') + 1));
        mp.clear ();

        nextLine = mapWriter.ReadLine ();
        mp.Rounds = NumberConverter.ConvertToInt (nextLine.Substring (nextLine.IndexOf(':') + 1));

        nextLine = mapWriter.ReadLine();
        mp.PlayedRounds = NumberConverter.ConvertToInt(nextLine.Substring(nextLine.IndexOf(':') + 1));

        nextLine = mapWriter.ReadLine();
        mp.Index = NumberConverter.ConvertToInt(nextLine.Substring(nextLine.IndexOf(':') + 1));

        nextLine = mapWriter.ReadLine();
        mp.ActiveTime = NumberConverter.ConvertToFloat (nextLine.Substring(nextLine.IndexOf(':') + 1));

        nextLine = mapWriter.ReadLine();
        mp.Mode = nextLine.Substring (nextLine.IndexOf(':') + 1);

        nextLine = mapWriter.ReadLine();
        numObjects = NumberConverter.ConvertToInt (nextLine.Substring(nextLine.IndexOf (':') + 1));

        for (int i = 0; i < numObjects; i++)
        {
            nextLine = mapWriter.ReadLine();
            row = nextLine.Substring(0, nextLine.IndexOf(':'));
            column = nextLine.Substring(nextLine.IndexOf(':') + 1, nextLine.IndexOf(',') - (row.Length + 1));
            type = nextLine.Substring(nextLine.IndexOf(',') + 1, nextLine.IndexOf('|') - (row.Length + column.Length + 2));
            name = nextLine.Substring(nextLine.IndexOf('|') + 1);

            lineToAdd = "";

            if (type.Equals(@"object"))
            {

                currentReader = new StreamReader(@"Assets/Resources/Data/SaveData/Slot1/Objects/"
                                                  + row + ',' + column + @".txt");
                //currentObj = ;

                lineToAdd = "";

                //NAME
                lineToAdd += currentReader.ReadLine() + '\n';

                //SEARCH NAME
                lineToAdd += currentReader.ReadLine() + '\n';

                //CONNECTED SKILL
                lineToAdd += currentReader.ReadLine() + '\n';

                //STRENGTH
                lineToAdd += currentReader.ReadLine() + '\n';

                //HEIGHT
                lineToAdd += currentReader.ReadLine() + '\n';

                //TIME REMAINING
                lineToAdd += currentReader.ReadLine() + '\n';

                //CONDITIONS
                lineToAdd += currentReader.ReadLine() + '\n';

                //OWNER
                lineToAdd += currentReader.ReadLine() + '\n';

                currentObjects.Add(lineToAdd);
                currentReader.Close();
            }
            else if (type.Equals(@"player"))
            {
                player = loadSavedPlayer (name, @"Assets/Resources/Data/SaveData/Slot1/Players/"
                                          + row + @"," + column + @" " + name + @".txt", true);

                mp.addPlayer (player);
            }

        }

        int rw, col;
        for (int z = 0; z < mp.Roster.Count; z ++) {
            player = (Player)mp.Roster[z];

            rw = player.Row;
            col = player.Column;
        }
        return mp;

    }

	public void saveMatch (Map mp)
	{
		StreamWriter mapWriter = new StreamWriter (@"Assets/Resources/Data/SaveData/Slot1/Map.txt"),
		objectLister = new StreamWriter (@"Assets/Resources/Data/SaveData/Slot1/Objects/All.txt"),
		playerLister = new StreamWriter (@"Assets/Resources/Data/SaveData/Slot1/Players/All.txt"),
		skillLister = new StreamWriter (@"Assets/Resources/Data/SaveData/Slot1/Skills/All.txt"),
		objectWriter = null, playerWriter = null;

        //StreamWriter modeReader = new StreamWriter (@"Assets/Resources/Data/Current/Mode.txt");
        //modeReader.WriteLine ("saved");
        //modeReader.Close ();

		Locatable currentObj;
        Player player;
        MapObject obj;
        Skill skill;
		ArrayList currentObjects = new ArrayList ();

		mapWriter.WriteLine (mp.ToString ());

        playerLister.WriteLine (mp.Roster.Count + @":PLAYERS"); 
        objectLister.WriteLine ((mp.Objects.Count - mp.Roster.Count) + @":OBJECTS"); 

		for (int i = 0; i < mp.Objects.Count; i++)
		{
			currentObj = (Locatable)mp.Objects [i];

			if (!currentObjects.Contains (currentObj)) {
				if (!currentObj.sentient ()) {
                    obj = (MapObject)currentObj;
					objectLister.WriteLine (obj.row () + @"," + obj.column ());
					objectWriter = new StreamWriter (@"Assets/Resources/Data/SaveData/Slot1/Objects/"
					                                 + obj.row () + @"," + obj.column () + @".txt");
					objectWriter.WriteLine (obj.ToString ());
					objectWriter.Close ();

				} else {
                    player = (Player)currentObj;
					playerLister.WriteLine (player.row () + @"," + player.column () + @" " + player.SearchName);
					playerWriter = new StreamWriter(@"Assets/Resources/Data/SaveData/Slot1/Players/"
                                                    + player.Row + @"," + player.Column + @" " +
                                                    player.SearchName + @".txt");
					playerWriter.WriteLine (((Player)currentObj).ToString ());

                    for (int j = 0; j < player.AllSkills.Count; j ++) {
                        skill = (Skill)player.AllSkills [j];
                        saveSkill (skill, player, @"Skills");
                    }
                    for (int j = 0; j < player.TrackSkills.Count; j ++) {
                        skill = (Skill)player.TrackSkills [i];
                        saveSkill (skill, player, @"Skills/Track"); 
                    }
                    for (int j = 0; j < player.TimeActivatedSkills.Count; j++)
                    {
                        skill = (Skill)player.TimeActivatedSkills[i];
                        saveSkill(skill, player, @"Skills/TimeActivated");
                    }

					playerWriter.Close ();
				}
			}
		}
        objectLister.Close ();
        playerLister.Close ();
        skillLister.Close();

        skillLister = new StreamWriter (@"Assets/Resources/Data/SaveData/Slot1/Interactables/All.txt");
        skillLister.WriteLine (mp.InteractableSize + @":INTERACTABLES");

        for (int i = 0; i < mp.InteractableSize; i ++) {
            skill = (Skill)mp.Interactables [i];
            if (!skill.Benign && !skill.Projectile)
            {
                skillLister.WriteLine(@"" + skill.CurrentLocation.Row + @"," + skill.CurrentLocation.Column + @" "
                                       + skill.SearchName);
                saveSkill(skill, skill.Owner, @"Interactables");
            }
        }

        skillLister.Close();

        skillLister = new StreamWriter(@"Assets/Resources/Data/SaveData/Slot1/Projectiles/All.txt");
        skillLister.WriteLine(mp.Projectiles.Count + @":PROJECTILES");

        for (int i = 0; i < mp.Projectiles.Count; i++)
        {
            skill = (Skill)mp.Projectiles [i];
            skillLister.WriteLine(@"" + skill.CurrentLocation.Row + @"," + skill.CurrentLocation.Column + @" "
                                   + skill.SearchName);
            saveSkill (skill, skill.Owner, @"Projectiles");
        }

        skillLister.Close ();

        skillLister = new StreamWriter(@"Assets/Resources/Data/SaveData/Slot1/Benigns/All.txt");
        skillLister.WriteLine (mp.BenignSize + @":BENIGNS");

        for (int i = 0; i < mp.Interactables.Count; i++)
        {

            skill = (Skill)mp.Interactables [i];
            if (skill.Benign)
            {
                skillLister.WriteLine(@"" + skill.CurrentLocation.Row + @"," + skill.CurrentLocation.Column + @" "
                                       + skill.SearchName);
                saveSkill(skill, skill.Owner, @"Benigns");
            }
        }

        skillLister.Close();

		mapWriter.Close ();
		objectLister.Close ();
		playerLister.Close ();

		if (objectWriter != null) objectWriter.Close ();
		if (playerWriter != null) playerWriter.Close ();
	}

    public void saveSkill (Skill s, Player p, string direct) {
        StreamWriter skillWriter = new StreamWriter (@"Assets/Resources/Data/SaveData/Slot1/" + direct + "/"
                                   + p.Row + @"," + p.Column + @" "
                                   + p.SearchName + @" " + s.SearchName + @".txt");
        skillWriter.WriteLine (s.InformationOutput);

        skillWriter.Close();


        for (int i = 0; i < s.LinkSkills.Count; i ++) {
            saveSkill ((Skill)s.LinkSkills [i], p, direct);
        }
        for (int i = 0; i < s.LearnSkills.Count; i ++) {
            saveSkill ((Skill)s.LearnSkills [i], p, direct);
        }
        for (int i = 0; i < s.ForgetSkills.Count; i ++) {
            saveSkill ((Skill)s.ForgetSkills [i], p, direct);
        }
        if (s.CounterSkill != null) {
            saveSkill (s.CounterSkill, p, direct);
        }
        if (s.TimerSkill != null) {
            saveSkill (s.TimerSkill, p, direct);
        }
        if (s.TrapSkill != null) {
            saveSkill (s.TrapSkill, p, direct);
        }
        if (s.DropItem != null) {
            saveSkill (s.DropItem, p, direct);
        }
    }
}






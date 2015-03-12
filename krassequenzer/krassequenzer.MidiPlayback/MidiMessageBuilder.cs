using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace krassequenzer.MidiPlayback
{
	public static class MidiMessageBuilder
	{
		private static uint EncodeGeneric(int statusMask, int channel, int d0, int d1)
		{
			// we assume that the caller has checked the arguments
			unchecked
			{
				uint u = (uint)statusMask |
					((uint)channel & 0xf) |
					(((uint)d0 & 0x7f) << 8) |
					(((uint)d1 & 0x7f) << 16);
				return u;
			}
		}

		private const int NoteOnStatus = 0x90;
		private const int NoteOffStatus = 0x80;
		private const int ControlChangeStatus = 0xb0;
		private const int ProgramChangeStatus = 0xc0;
		private const int PitchBendStatus = 0xe0;
		private const int PolyphonicKeyPressureStatus = 0xa0;
		private const int ChannelPressureStatus = 0xd0;

		public static uint NoteOn(int channel, int key, int velocity)
		{
			uint u = EncodeGeneric(NoteOnStatus, channel, key, velocity);
			return u;
		}

		public static uint NoteOff(int channel, int key, int velocity)
		{
			uint u = EncodeGeneric(NoteOffStatus, channel, key, velocity);
			return u;
		}

		public static uint ControlChange(int channel, int controller, int value)
		{
			uint u = EncodeGeneric(ControlChangeStatus, channel, controller, value);
			return u;
		}

		public static uint ProgramChange(int channel, int program)
		{
			uint u = EncodeGeneric(ProgramChangeStatus, channel, program, 0);
			return u;
		}

#warning TODO dat comment tho
		/// <summary>
		/// 
		/// </summary>
		/// <param name="channel"></param>
		/// <param name="value">Range -8192 .. 8191</param>
		public static uint PitchBend(int channel, int value)
		{
			value += 0x2000;
			uint u = EncodeGeneric(PitchBendStatus, channel, value, value >> 7);
			return u;
		}

#warning TODO dat comment tho
		/// <summary>
		/// Aftertouch
		/// </summary>
		/// <param name="channel"></param>
		/// <param name="key"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static uint PolyphonicKeyPressure(int channel, int key, int value)
		{
			uint u = EncodeGeneric(PolyphonicKeyPressureStatus, channel, key, value);
			return u;
		}

		public static uint ChannelPressure(int channel, int value)
		{
			uint u = EncodeGeneric(ChannelPressureStatus, channel, value, 0);
			return u;
		}

		public static byte[] Tempo(int usecPerQuarter)
		{
			var bytes = new byte[6];
			bytes[0] = 0xff;
			bytes[1] = 0x51;
			bytes[2] = 0x03;
			bytes[3] = (byte)((usecPerQuarter >> 16) & 0xff);
			bytes[4] = (byte)((usecPerQuarter >> 8) & 0xff);
			bytes[5] = (byte)(usecPerQuarter & 0xff);
			return bytes;
		}
	}

	public enum MidiGMInstrumentSet
	{
		/// <summary>
		/// Acoustic Grand Piano
		/// </summary>
		Acoustic_Grand_Piano = 0,
		/// <summary>
		/// Bright Acoustic Piano
		/// </summary>
		Bright_Acoustic_Piano = 1,
		/// <summary>
		/// Electric Grand Piano
		/// </summary>
		Electric_Grand_Piano = 2,
		/// <summary>
		/// Honky-tonk Piano
		/// </summary>
		Honkytonk_Piano = 3,
		/// <summary>
		/// Electric Piano 1
		/// </summary>
		Electric_Piano_1 = 4,
		/// <summary>
		/// Electric Piano 2
		/// </summary>
		Electric_Piano_2 = 5,
		/// <summary>
		/// Harpsichord
		/// </summary>
		Harpsichord = 6,
		/// <summary>
		/// Clavi
		/// </summary>
		Clavi = 7,
		/// <summary>
		/// Celesta
		/// </summary>
		Celesta = 8,
		/// <summary>
		/// Glockenspiel
		/// </summary>
		Glockenspiel = 9,
		/// <summary>
		/// Music Box
		/// </summary>
		Music_Box = 10,
		/// <summary>
		/// Vibraphone
		/// </summary>
		Vibraphone = 11,
		/// <summary>
		/// Marimba
		/// </summary>
		Marimba = 12,
		/// <summary>
		/// Xylophone
		/// </summary>
		Xylophone = 13,
		/// <summary>
		/// Tubular Bells
		/// </summary>
		Tubular_Bells = 14,
		/// <summary>
		/// Dulcimer
		/// </summary>
		Dulcimer = 15,
		/// <summary>
		/// Drawbar Organ
		/// </summary>
		Drawbar_Organ = 16,
		/// <summary>
		/// Percussive Organ
		/// </summary>
		Percussive_Organ = 17,
		/// <summary>
		/// Rock Organ
		/// </summary>
		Rock_Organ = 18,
		/// <summary>
		/// Church Organ
		/// </summary>
		Church_Organ = 19,
		/// <summary>
		/// Reed Organ
		/// </summary>
		Reed_Organ = 20,
		/// <summary>
		/// Accordion
		/// </summary>
		Accordion = 21,
		/// <summary>
		/// Harmonica
		/// </summary>
		Harmonica = 22,
		/// <summary>
		/// Tango Accordion
		/// </summary>
		Tango_Accordion = 23,
		/// <summary>
		/// Acoustic Guitar (nylon)
		/// </summary>
		Acoustic_Guitar_nylon = 24,
		/// <summary>
		/// Acoustic Guitar (steel)
		/// </summary>
		Acoustic_Guitar_steel = 25,
		/// <summary>
		/// Electric Guitar (jazz)
		/// </summary>
		Electric_Guitar_jazz = 26,
		/// <summary>
		/// Electric Guitar (clean)
		/// </summary>
		Electric_Guitar_clean = 27,
		/// <summary>
		/// Electric Guitar (muted)
		/// </summary>
		Electric_Guitar_muted = 28,
		/// <summary>
		/// Overdriven Guitar
		/// </summary>
		Overdriven_Guitar = 29,
		/// <summary>
		/// Distortion Guitar
		/// </summary>
		Distortion_Guitar = 30,
		/// <summary>
		/// Guitar harmonics
		/// </summary>
		Guitar_harmonics = 31,
		/// <summary>
		/// Acoustic Bass
		/// </summary>
		Acoustic_Bass = 32,
		/// <summary>
		/// Electric Bass (finger)
		/// </summary>
		Electric_Bass_finger = 33,
		/// <summary>
		/// Electric Bass (pick)
		/// </summary>
		Electric_Bass_pick = 34,
		/// <summary>
		/// Fretless Bass
		/// </summary>
		Fretless_Bass = 35,
		/// <summary>
		/// Slap Bass 1
		/// </summary>
		Slap_Bass_1 = 36,
		/// <summary>
		/// Slap Bass 2
		/// </summary>
		Slap_Bass_2 = 37,
		/// <summary>
		/// Synth Bass 1
		/// </summary>
		Synth_Bass_1 = 38,
		/// <summary>
		/// Synth Bass 2
		/// </summary>
		Synth_Bass_2 = 39,
		/// <summary>
		/// Violin
		/// </summary>
		Violin = 40,
		/// <summary>
		/// Viola
		/// </summary>
		Viola = 41,
		/// <summary>
		/// Cello
		/// </summary>
		Cello = 42,
		/// <summary>
		/// Contrabass
		/// </summary>
		Contrabass = 43,
		/// <summary>
		/// Tremolo Strings
		/// </summary>
		Tremolo_Strings = 44,
		/// <summary>
		/// Pizzicato Strings
		/// </summary>
		Pizzicato_Strings = 45,
		/// <summary>
		/// Orchestral Harp
		/// </summary>
		Orchestral_Harp = 46,
		/// <summary>
		/// Timpani
		/// </summary>
		Timpani = 47,
		/// <summary>
		/// String Ensemble 1
		/// </summary>
		String_Ensemble_1 = 48,
		/// <summary>
		/// String Ensemble 2
		/// </summary>
		String_Ensemble_2 = 49,
		/// <summary>
		/// SynthStrings 1
		/// </summary>
		SynthStrings_1 = 50,
		/// <summary>
		/// SynthStrings 2
		/// </summary>
		SynthStrings_2 = 51,
		/// <summary>
		/// Choir Aahs
		/// </summary>
		Choir_Aahs = 52,
		/// <summary>
		/// Voice Oohs
		/// </summary>
		Voice_Oohs = 53,
		/// <summary>
		/// Synth Voice
		/// </summary>
		Synth_Voice = 54,
		/// <summary>
		/// Orchestra Hit
		/// </summary>
		Orchestra_Hit = 55,
		/// <summary>
		/// Trumpet
		/// </summary>
		Trumpet = 56,
		/// <summary>
		/// Trombone
		/// </summary>
		Trombone = 57,
		/// <summary>
		/// Tuba
		/// </summary>
		Tuba = 58,
		/// <summary>
		/// Muted Trumpet
		/// </summary>
		Muted_Trumpet = 59,
		/// <summary>
		/// French Horn
		/// </summary>
		French_Horn = 60,
		/// <summary>
		/// Brass Section
		/// </summary>
		Brass_Section = 61,
		/// <summary>
		/// SynthBrass 1
		/// </summary>
		SynthBrass_1 = 62,
		/// <summary>
		/// SynthBrass 2
		/// </summary>
		SynthBrass_2 = 63,
		/// <summary>
		/// Soprano Sax
		/// </summary>
		Soprano_Sax = 64,
		/// <summary>
		/// Alto Sax
		/// </summary>
		Alto_Sax = 65,
		/// <summary>
		/// Tenor Sax
		/// </summary>
		Tenor_Sax = 66,
		/// <summary>
		/// Baritone Sax
		/// </summary>
		Baritone_Sax = 67,
		/// <summary>
		/// Oboe
		/// </summary>
		Oboe = 68,
		/// <summary>
		/// English Horn
		/// </summary>
		English_Horn = 69,
		/// <summary>
		/// Bassoon
		/// </summary>
		Bassoon = 70,
		/// <summary>
		/// Clarinet
		/// </summary>
		Clarinet = 71,
		/// <summary>
		/// Piccolo
		/// </summary>
		Piccolo = 72,
		/// <summary>
		/// Flute
		/// </summary>
		Flute = 73,
		/// <summary>
		/// Recorder
		/// </summary>
		Recorder = 74,
		/// <summary>
		/// Pan Flute
		/// </summary>
		Pan_Flute = 75,
		/// <summary>
		/// Blown Bottle
		/// </summary>
		Blown_Bottle = 76,
		/// <summary>
		/// Shakuhachi
		/// </summary>
		Shakuhachi = 77,
		/// <summary>
		/// Whistle
		/// </summary>
		Whistle = 78,
		/// <summary>
		/// Ocarina
		/// </summary>
		Ocarina = 79,
		/// <summary>
		/// Lead 1 (square)
		/// </summary>
		Lead_1_square = 80,
		/// <summary>
		/// Lead 2 (sawtooth)
		/// </summary>
		Lead_2_sawtooth = 81,
		/// <summary>
		/// Lead 3 (calliope)
		/// </summary>
		Lead_3_calliope = 82,
		/// <summary>
		/// Lead 4 (chiff)
		/// </summary>
		Lead_4_chiff = 83,
		/// <summary>
		/// Lead 5 (charang)
		/// </summary>
		Lead_5_charang = 84,
		/// <summary>
		/// Lead 6 (voice)
		/// </summary>
		Lead_6_voice = 85,
		/// <summary>
		/// Lead 7 (fifths)
		/// </summary>
		Lead_7_fifths = 86,
		/// <summary>
		/// Lead 8 (bass + lead)
		/// </summary>
		Lead_8_bass__lead = 87,
		/// <summary>
		/// Pad 1 (new age)
		/// </summary>
		Pad_1_new_age = 88,
		/// <summary>
		/// Pad 2 (warm)
		/// </summary>
		Pad_2_warm = 89,
		/// <summary>
		/// Pad 3 (polysynth)
		/// </summary>
		Pad_3_polysynth = 90,
		/// <summary>
		/// Pad 4 (choir)
		/// </summary>
		Pad_4_choir = 91,
		/// <summary>
		/// Pad 5 (bowed)
		/// </summary>
		Pad_5_bowed = 92,
		/// <summary>
		/// Pad 6 (metallic)
		/// </summary>
		Pad_6_metallic = 93,
		/// <summary>
		/// Pad 7 (halo)
		/// </summary>
		Pad_7_halo = 94,
		/// <summary>
		/// Pad 8 (sweep)
		/// </summary>
		Pad_8_sweep = 95,
		/// <summary>
		/// FX 1 (rain)
		/// </summary>
		FX_1_rain = 96,
		/// <summary>
		/// FX 2 (soundtrack)
		/// </summary>
		FX_2_soundtrack = 97,
		/// <summary>
		/// FX 3 (crystal)
		/// </summary>
		FX_3_crystal = 98,
		/// <summary>
		/// FX 4 (atmosphere)
		/// </summary>
		FX_4_atmosphere = 99,
		/// <summary>
		/// FX 5 (brightness)
		/// </summary>
		FX_5_brightness = 100,
		/// <summary>
		/// FX 6 (goblins)
		/// </summary>
		FX_6_goblins = 101,
		/// <summary>
		/// FX 7 (echoes)
		/// </summary>
		FX_7_echoes = 102,
		/// <summary>
		/// FX 8 (sci-fi)
		/// </summary>
		FX_8_scifi = 103,
		/// <summary>
		/// Sitar
		/// </summary>
		Sitar = 104,
		/// <summary>
		/// Banjo
		/// </summary>
		Banjo = 105,
		/// <summary>
		/// Shamisen
		/// </summary>
		Shamisen = 106,
		/// <summary>
		/// Koto
		/// </summary>
		Koto = 107,
		/// <summary>
		/// Kalimba
		/// </summary>
		Kalimba = 108,
		/// <summary>
		/// Bag pipe
		/// </summary>
		Bag_pipe = 109,
		/// <summary>
		/// Fiddle
		/// </summary>
		Fiddle = 110,
		/// <summary>
		/// Shanai
		/// </summary>
		Shanai = 111,
		/// <summary>
		/// Tinkle Bell
		/// </summary>
		Tinkle_Bell = 112,
		/// <summary>
		/// Agogo
		/// </summary>
		Agogo = 113,
		/// <summary>
		/// Steel Drums
		/// </summary>
		Steel_Drums = 114,
		/// <summary>
		/// Woodblock
		/// </summary>
		Woodblock = 115,
		/// <summary>
		/// Taiko Drum
		/// </summary>
		Taiko_Drum = 116,
		/// <summary>
		/// Melodic Tom
		/// </summary>
		Melodic_Tom = 117,
		/// <summary>
		/// Synth Drum
		/// </summary>
		Synth_Drum = 118,
		/// <summary>
		/// Reverse Cymbal
		/// </summary>
		Reverse_Cymbal = 119,
		/// <summary>
		/// Guitar Fret Noise
		/// </summary>
		Guitar_Fret_Noise = 120,
		/// <summary>
		/// Breath Noise
		/// </summary>
		Breath_Noise = 121,
		/// <summary>
		/// Seashore
		/// </summary>
		Seashore = 122,
		/// <summary>
		/// Bird Tweet
		/// </summary>
		Bird_Tweet = 123,
		/// <summary>
		/// Telephone Ring
		/// </summary>
		Telephone_Ring = 124,
		/// <summary>
		/// Helicopter
		/// </summary>
		Helicopter = 125,
		/// <summary>
		/// Applause
		/// </summary>
		Applause = 126,
		/// <summary>
		/// Gunshot
		/// </summary>
		Gunshot = 127, 
	}
}

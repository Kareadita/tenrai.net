using System.ComponentModel;

namespace Tenrai;

/// <summary>
/// A staff job MyAnimeList credits on an anime.
/// </summary>
public enum StaffPosition
{
	/// <summary>
	/// Director.
	/// </summary>
	[Description("director")]
	Director,

	/// <summary>
	/// Co-Director.
	/// </summary>
	[Description("co_director")]
	CoDirector,

	/// <summary>
	/// Assistant Director.
	/// </summary>
	[Description("assistant_director")]
	AssistantDirector,

	/// <summary>
	/// Episode Director.
	/// </summary>
	[Description("episode_director")]
	EpisodeDirector,

	/// <summary>
	/// Storyboard.
	/// </summary>
	[Description("storyboard")]
	Storyboard,

	/// <summary>
	/// Series Composition.
	/// </summary>
	[Description("series_composition")]
	SeriesComposition,

	/// <summary>
	/// Script.
	/// </summary>
	[Description("script")]
	Script,

	/// <summary>
	/// Screenplay.
	/// </summary>
	[Description("screenplay")]
	Screenplay,

	/// <summary>
	/// Original Creator.
	/// </summary>
	[Description("original_creator")]
	OriginalCreator,

	/// <summary>
	/// Creator.
	/// </summary>
	[Description("creator")]
	Creator,

	/// <summary>
	/// Planning.
	/// </summary>
	[Description("planning")]
	Planning,

	/// <summary>
	/// Producer.
	/// </summary>
	[Description("producer")]
	Producer,

	/// <summary>
	/// Executive Producer.
	/// </summary>
	[Description("executive_producer")]
	ExecutiveProducer,

	/// <summary>
	/// Chief Producer.
	/// </summary>
	[Description("chief_producer")]
	ChiefProducer,

	/// <summary>
	/// Co-Producer.
	/// </summary>
	[Description("co_producer")]
	CoProducer,

	/// <summary>
	/// Associate Producer.
	/// </summary>
	[Description("associate_producer")]
	AssociateProducer,

	/// <summary>
	/// Assistant Producer.
	/// </summary>
	[Description("assistant_producer")]
	AssistantProducer,

	/// <summary>
	/// Planning Producer.
	/// </summary>
	[Description("planning_producer")]
	PlanningProducer,

	/// <summary>
	/// Production Manager.
	/// </summary>
	[Description("production_manager")]
	ProductionManager,

	/// <summary>
	/// Production Assistant.
	/// </summary>
	[Description("production_assistant")]
	ProductionAssistant,

	/// <summary>
	/// Production Coordination.
	/// </summary>
	[Description("production_coordination")]
	ProductionCoordination,

	/// <summary>
	/// Assistant Production Coordinator.
	/// </summary>
	[Description("assistant_production_coordinator")]
	AssistantProductionCoordinator,

	/// <summary>
	/// Series Production Director.
	/// </summary>
	[Description("series_production_director")]
	SeriesProductionDirector,

	/// <summary>
	/// Publicity.
	/// </summary>
	[Description("publicity")]
	Publicity,

	/// <summary>
	/// Casting Director.
	/// </summary>
	[Description("casting_director")]
	CastingDirector,

	/// <summary>
	/// Associate Casting Director.
	/// </summary>
	[Description("associate_casting_director")]
	AssociateCastingDirector,

	/// <summary>
	/// Character Design.
	/// </summary>
	[Description("character_design")]
	CharacterDesign,

	/// <summary>
	/// Original Character Design.
	/// </summary>
	[Description("original_character_design")]
	OriginalCharacterDesign,

	/// <summary>
	/// Mechanical Design.
	/// </summary>
	[Description("mechanical_design")]
	MechanicalDesign,

	/// <summary>
	/// Chief Animation Director.
	/// </summary>
	[Description("chief_animation_director")]
	ChiefAnimationDirector,

	/// <summary>
	/// Animation Director.
	/// </summary>
	[Description("animation_director")]
	AnimationDirector,

	/// <summary>
	/// Assistant Animation Director.
	/// </summary>
	[Description("assistant_animation_director")]
	AssistantAnimationDirector,

	/// <summary>
	/// Key Animation.
	/// </summary>
	[Description("key_animation")]
	KeyAnimation,

	/// <summary>
	/// 2nd Key Animation.
	/// </summary>
	[Description("2nd_key_animation")]
	SecondKeyAnimation,

	/// <summary>
	/// In-Between Animation.
	/// </summary>
	[Description("in_between_animation")]
	InBetweenAnimation,

	/// <summary>
	/// Animation Check.
	/// </summary>
	[Description("animation_check")]
	AnimationCheck,

	/// <summary>
	/// Layout.
	/// </summary>
	[Description("layout")]
	Layout,

	/// <summary>
	/// Principle Drawing.
	/// </summary>
	[Description("principle_drawing")]
	PrincipleDrawing,

	/// <summary>
	/// Art Director.
	/// </summary>
	[Description("art_director")]
	ArtDirector,

	/// <summary>
	/// Background Art.
	/// </summary>
	[Description("background_art")]
	BackgroundArt,

	/// <summary>
	/// Color Design.
	/// </summary>
	[Description("color_design")]
	ColorDesign,

	/// <summary>
	/// Color Setting.
	/// </summary>
	[Description("color_setting")]
	ColorSetting,

	/// <summary>
	/// Digital Paint.
	/// </summary>
	[Description("digital_paint")]
	DigitalPaint,

	/// <summary>
	/// Special Effects.
	/// </summary>
	[Description("special_effects")]
	SpecialEffects,

	/// <summary>
	/// Director of Photography.
	/// </summary>
	[Description("director_of_photography")]
	DirectorOfPhotography,

	/// <summary>
	/// Editing.
	/// </summary>
	[Description("editing")]
	Editing,

	/// <summary>
	/// Online Editor.
	/// </summary>
	[Description("online_editor")]
	OnlineEditor,

	/// <summary>
	/// Online Editing Supervision.
	/// </summary>
	[Description("online_editing_supervision")]
	OnlineEditingSupervision,

	/// <summary>
	/// Setting.
	/// </summary>
	[Description("setting")]
	Setting,

	/// <summary>
	/// Setting Manager.
	/// </summary>
	[Description("setting_manager")]
	SettingManager,

	/// <summary>
	/// Spotting.
	/// </summary>
	[Description("spotting")]
	Spotting,

	/// <summary>
	/// Music.
	/// </summary>
	[Description("music")]
	Music,

	/// <summary>
	/// Sound Director.
	/// </summary>
	[Description("sound_director")]
	SoundDirector,

	/// <summary>
	/// Sound Effects.
	/// </summary>
	[Description("sound_effects")]
	SoundEffects,

	/// <summary>
	/// Sound Manager.
	/// </summary>
	[Description("sound_manager")]
	SoundManager,

	/// <summary>
	/// Sound Supervisor.
	/// </summary>
	[Description("sound_supervisor")]
	SoundSupervisor,

	/// <summary>
	/// Recording.
	/// </summary>
	[Description("recording")]
	Recording,

	/// <summary>
	/// Recording Engineer.
	/// </summary>
	[Description("recording_engineer")]
	RecordingEngineer,

	/// <summary>
	/// Recording Assistant.
	/// </summary>
	[Description("recording_assistant")]
	RecordingAssistant,

	/// <summary>
	/// Assistant Engineer.
	/// </summary>
	[Description("assistant_engineer")]
	AssistantEngineer,

	/// <summary>
	/// Re-Recording Mixing.
	/// </summary>
	[Description("re_recording_mixing")]
	ReRecordingMixing,

	/// <summary>
	/// Dialogue Editing.
	/// </summary>
	[Description("dialogue_editing")]
	DialogueEditing,

	/// <summary>
	/// ADR Director.
	/// </summary>
	[Description("adr_director")]
	AdrDirector,

	/// <summary>
	/// Post-Production Assistant.
	/// </summary>
	[Description("post_production_assistant")]
	PostProductionAssistant,

	/// <summary>
	/// Theme Song Performance.
	/// </summary>
	[Description("theme_song_performance")]
	ThemeSongPerformance,

	/// <summary>
	/// Theme Song Composition.
	/// </summary>
	[Description("theme_song_composition")]
	ThemeSongComposition,

	/// <summary>
	/// Theme Song Lyrics.
	/// </summary>
	[Description("theme_song_lyrics")]
	ThemeSongLyrics,

	/// <summary>
	/// Theme Song Arrangement.
	/// </summary>
	[Description("theme_song_arrangement")]
	ThemeSongArrangement,

	/// <summary>
	/// Inserted Song Performance.
	/// </summary>
	[Description("inserted_song_performance")]
	InsertedSongPerformance
}

using DOTSAnimation;
using Unity.Entities;

[DisableAutoCreation]
public partial class StateMachineExampleUISystem : SystemBase
{
    private static int IsJumpingHash => StateMachineParameterUtils.GetHashCode("IsJumping");
    private static int IsFallingHash => StateMachineParameterUtils.GetHashCode("IsFalling");
    private static int SpeedHash => StateMachineParameterUtils.GetHashCode("Speed");
    
    private bool playCircleSlash = false;
    private bool playSlash = false;

    protected override void OnStartRunning()
    {
        base.OnStartRunning();
        var ui = this.GetSingleton<StateMachineExampleUI>();
        ui.AtkButton.onClick.AddListener(OnAttackButtonPressed);
        ui.AtkRmButton.onClick.AddListener(OnAttackRmButtonPressed);
    }
    private void OnAttackRmButtonPressed()
    {
        playCircleSlash = true;
    }

    private void OnAttackButtonPressed()
    {
        playSlash = true;
    }

    protected override void OnUpdate()
    {
        var oneShots = GetSingleton<StateMachineExampleOneShots>();
        var stateMachineUI = this.GetSingleton<StateMachineExampleUI>();
        Entities
            .ForEach((
                ref PlayOneShotRequest playOneShot,
                ref DynamicBuffer<BlendParameter> blendParameters,
                ref DynamicBuffer<BoolParameter> boolParameters) =>
            {
                boolParameters.SetParameter(IsFallingHash, stateMachineUI.IsFallingToggle.isOn);
                boolParameters.SetParameter(IsJumpingHash, stateMachineUI.IsJumpingToggle.isOn);
                blendParameters.SetParameter(SpeedHash, stateMachineUI.BlendSlider.value);
                
                if (playSlash)
                {
                    playOneShot = new PlayOneShotRequest(
                        oneShots.Clips,
                        oneShots.ClipEvents,
                        oneShots.SlashClipIndex);
                }
                else if (playCircleSlash)
                {
                    playOneShot = new PlayOneShotRequest(
                        oneShots.Clips,
                        oneShots.ClipEvents,
                        oneShots.CircleSlashClipIndex);
                }

                playSlash = playCircleSlash = false;
            }).WithoutBurst().Run();
    }
}
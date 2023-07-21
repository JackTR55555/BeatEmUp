using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;

public class SkinManager : MonoBehaviour
{
	[HideInInspector] public Master master;

	Skin finalSkin;

	[SpineSkin] public string baseSkin;
	[SpineSkin] public string defenseSkin;
	[SpineSkin] public string functionSkin;
	[SpineSkin] public string attackSkin;

	public void SetSkin()
	{
        if (defenseSkin != master.DefenseCores[master.activeDefenseCore].skinToUse)
        {
			defenseSkin = master.DefenseCores[master.activeDefenseCore].skinToUse;
			PrepareSkin();
			ShowSkin();
		}
		if (functionSkin != master.FunctionCores[master.activeFunctionCore].skinToUse)
		{
			functionSkin = master.FunctionCores[master.activeFunctionCore].skinToUse;
			PrepareSkin();
			ShowSkin();
		}
		if (attackSkin != master.AttackCores[master.activeAttackCore].skinToUse)
		{
			attackSkin = master.AttackCores[master.activeAttackCore].skinToUse;
			PrepareSkin();
			ShowSkin();
		}
	}

	public void PrepareSkin()
	{
		Skeleton skeleton = master.skeletonAnimation.Skeleton;
		SkeletonData skeletonData = skeleton.Data;

		finalSkin = new Skin("final");

		finalSkin.AddSkin(skeletonData.FindSkin(baseSkin));
		finalSkin.AddSkin(skeletonData.FindSkin(defenseSkin));
		finalSkin.AddSkin(skeletonData.FindSkin(functionSkin));
		finalSkin.AddSkin(skeletonData.FindSkin(attackSkin));
	}
	public void ShowSkin()
	{
		Skeleton skeleton = master.skeletonAnimation.Skeleton;
		SkeletonData skeletonData = skeleton.Data;
		skeleton.SetSkin(finalSkin);
		skeleton.SetSlotsToSetupPose();
	}
}

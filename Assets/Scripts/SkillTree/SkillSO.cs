using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSkill", menuName = "SkillTree/Skill")]//这个属性允许我们在Unity编辑器中通过右键菜单创建新的技能数据对象
public class SkillSO : ScriptableObject
{
  
    public string skillName;//技能名称
    public int maxLevel;//技能最大等级
    public Sprite skillIcon;//技能图标
}

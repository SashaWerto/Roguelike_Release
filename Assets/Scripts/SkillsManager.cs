using System.Collections.Generic;
using UnityEngine;

public class SkillsManager : MonoBehaviour
{
    public List<Skill_Cell> skillCells;
    private static SkillsManager skillsManager;
    public static SkillsManager Instance => skillsManager;

    private void Awake()
    {
        skillsManager = this;
    }
}

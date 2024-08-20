﻿using AinAlAtaaFoundation.Models;

namespace AinAlAtaaFoundation.Shared.Abstraction
{
    public interface IFilterParameters
    {
        public Clan Clan { get; }
        public Branch Branch { get; }
        public BranchRepresentative BranchRepresentative { get; }
        public District District { get; }
        public DistrictRepresentative DistrictRepresentative { get; }
        public FamilyType FamilyType { get; }
        public SocialStatus SocialStatus { get; }
        public OrphanType OrphanType { get; }
    }
}
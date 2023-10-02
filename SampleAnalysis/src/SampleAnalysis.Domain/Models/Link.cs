﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SampleAnalysis.Domain.Models;

public partial class Link
{
    [Key]
    public Guid Id { get; set; }

    public Guid ParentEventFrameId { get; set; }

    public Guid ParentEventFrameTypeId { get; set; }

    public Guid ChildEventFrameId { get; set; }

    public Guid ChildEventFrameTypeId { get; set; }

    [ForeignKey("ChildEventFrameId")]
    [InverseProperty("LinkChildEventFrames")]
    public virtual EventFrame ChildEventFrame { get; set; }

    [ForeignKey("ChildEventFrameTypeId")]
    [InverseProperty("LinkChildEventFrameTypes")]
    public virtual EventFrameType ChildEventFrameType { get; set; }

    [ForeignKey("ParentEventFrameId")]
    [InverseProperty("LinkParentEventFrames")]
    public virtual EventFrame ParentEventFrame { get; set; }

    [ForeignKey("ParentEventFrameTypeId")]
    [InverseProperty("LinkParentEventFrameTypes")]
    public virtual EventFrameType ParentEventFrameType { get; set; }
}
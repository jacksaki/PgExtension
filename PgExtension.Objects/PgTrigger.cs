using PgExtension.Query;

namespace PgExtension.Objects;

public class PgTrigger : IPgObject
{
    [DbColumn("trigger_schema")]
    public string SchemaName { get; private set; } = string.Empty;
    [DbColumn("trigger_name")]
    public string Name { get; private set; } = string.Empty;
    [DbColumn("event_manipulation")]
    public string? EventManipulation { get; private set; }
    [DbColumn("event_object_schema")]
    public string? EventObjectSchema { get; private set; }
    [DbColumn("event_object_table")]
    public string? EventObjectTableName { get; private set; }
    [DbColumn("event_object_table_oid")]
    public int? EventObjectTableOid { get; private set; }

    [DbColumn("action_order")]
    public int? ActionOrder { get; private set; }
    [DbColumn("action_condition")]
    public string? ActionCondition { get; private set; }
    [DbColumn("action_statement")]
    public string? ActionStatement { get; private set; }
    [DbColumn("action_orientation")]
    public string? ActionOrientation { get; private set; }
    [DbColumn("action_timing")]
    public string? ActionTiming { get; private set; }
    [DbColumn("action_reference_old_table")]
    public string? ActionReferenceOldTable { get; private set; }
    [DbColumn("action_reference_new_table")]
    public string? ActionReferenceNewTable { get; private set; }
}

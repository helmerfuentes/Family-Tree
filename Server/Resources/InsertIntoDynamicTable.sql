INSERT INTO {0} (ChildId, BiologicalChild, ChildFullNames)
SELECT @ChildId, @BiologicalChild, @ChildFullNames
WHERE NOT EXISTS (
    SELECT 1 FROM {0} WHERE ChildId = @ChildId
);

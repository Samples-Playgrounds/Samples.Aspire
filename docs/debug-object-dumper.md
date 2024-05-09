# Debug Object Dumper

https://github.com/ycherkes/ObjectDumper

https://marketplace.visualstudio.com/items?itemName=YevhenCherkes.YellowFlavorObjectDumper

https://marketplace.visualstudio.com/items?itemName=YevhenCherkes.object-dumper


replace `obj`with variable name

```csharp
(new System.Xml.Serialization.XmlSerializer(obj.GetType()))
    .Serialize(new System.IO.StreamWriter
            (
                $"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}/dump.xml"), 
                obj
            )
```

```csharp
System.Text.Json.JsonSerializer.Serialize(obj)
```

```csharp
System.IO.File.WriteAllText
                    (
                        $"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}/dump.1.json"), 
                        Newtonsoft.Json.JsonConvert.SerializeObject(obj)
                    )

```

```csharp
System.IO.File.WriteAllText
                    (
                        $"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}/dump.1.json"), 
                        System.Text.Json.JsonSerializer.Serialize(obj)
                    )
```
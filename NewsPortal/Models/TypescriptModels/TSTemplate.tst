${
    using Typewriter.Extensions.Types;

    // More info: http://frhagn.github.io/Typewriter/

    //import the non-primitive types
    string Imports(Class c){
        var importBaseClass = (c.BaseClass!=null) ? ( "import {" + c.BaseClass.ToString() +"} from \"./" + c.BaseClass.ToString() +"\";") :"";

        var importTypesClass = c.Properties.Where(p=>!p.Type.IsPrimitive || p.Type.IsEnum)
            .Select(p=> $"import {{ {p.Type.Name} }} from './{p.Type.Name}';").Distinct()
            .Aggregate("", (all,import) => $"{all}{import}\r\n").Replace("[]", "").TrimStart();

        return importBaseClass + importTypesClass;
    }

    //Inheritance
    string ClassNameWithExtends(Class c) {
        return c.Name +  (c.BaseClass!=null ?  " extends " + c.BaseClass.Name : "");
    }
}$Classes(*Model)[$Imports
export class $ClassNameWithExtends {
    $Properties[

    public $Name: $Type = $Type[$Default];]
}]
$Enums(*)[
export enum $Name { $Values[
    $Name = $Value][,]
}]
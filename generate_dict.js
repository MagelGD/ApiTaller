const fs = require('fs');
const path = require('path');

const modelsDir = "C:\\Users\\miguelagutierrezg\\Proyectos\\Api\\ApiTaller\\ApiTaller.Domain\\Models";
const outputFile = "C:\\Users\\miguelagutierrezg\\Proyectos\\Api\\ApiTaller\\diccionario.md";

const files = fs.readdirSync(modelsDir).filter(f => f.endsWith('.cs'));

let md = "### 2. Diccionario de Datos (Nivel DBA)\n\n";

files.forEach(file => {
    const content = fs.readFileSync(path.join(modelsDir, file), 'utf8');
    const entityName = file.replace('.cs', '');
    
    md += `#### Entidad: **${entityName}**\n\n`;
    md += `| Nombre del Campo | Tipo de Dato (C#) | Obligatorio | Llaves | Descripción Funcional Exhaustiva |\n`;
    md += `|---|---|---|---|---|\n`;

    // Basic regex to find properties: public [Type] [Name] { get; set; }
    const propRegex = /public\s+(virtual\s+)?([^\s]+)\s+([^\s]+)\s*\{\s*get;\s*set;\s*\}/g;
    let match;
    
    while ((match = propRegex.exec(content)) !== null) {
        const type = match[2];
        const name = match[3];
        
        let mandatory = "NOT NULL";
        if (type.includes('?')) mandatory = "NULL";
        else if (type === "string") mandatory = "NULL"; // Usually strings are nullable in older C# unless annotated, but we'll assume NULL.
        
        let isFk = "";
        if (name.endsWith("Id") && name !== "Id") isFk = "FK";
        if (name === "Id") isFk = "PK";

        let desc = `Almacena el valor para ${name} en el contexto de ${entityName}.`;
        
        // Try to find comments above property
        // This is a naive parse but effective for bulk
        md += `| ${name} | ${type} | ${mandatory} | ${isFk} | ${desc} |\n`;
    }
    
    md += "\n";
});

fs.writeFileSync(outputFile, md, 'utf8');
console.log("Dictionary generated at: " + outputFile);

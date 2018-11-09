# Tests

`keyword.operator.expression.of`

```javascript
for (const jsonTokenColor of jsonObj.tokenColors) {
  if (jsonTokenColor.settings && jsonTokenColor.settings.foreground) {
    colorScheme.tokenColors.push(this.retrieveTokenColor(jsonTokenColor, colorScheme.editorBackground));
  }
}
```

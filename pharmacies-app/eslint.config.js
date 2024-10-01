import globals from 'globals'
import pluginJs from '@eslint/js'
import tseslint from 'typescript-eslint'
import pluginReact from 'eslint-plugin-react'
import pluginPrettier from '@eslint/js'

export default [
    { files: ['**/*.{js,mjs,ts,jsx,tsx}'] },
    { languageOptions: { globals: globals.browser } },
    pluginJs.configs.recommended,
    ...tseslint.configs.recommended,
    pluginReact.configs.flat.recommended,
    pluginPrettier.configs.recommended,
    {
        rules: {
            'react/react-in-jsx-scope': 'off',
        },
    },
]

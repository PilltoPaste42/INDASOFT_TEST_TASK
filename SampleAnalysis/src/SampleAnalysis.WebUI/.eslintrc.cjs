module.exports = {
  root: true,
  extends: [
    'eslint:recommended',
    'preact',
    'plugin:prettier/recommended',
    'plugin:@typescript-eslint/recommended',
  ],
  plugins: [
    'prettier',
    '@typescript-eslint',
    ],
  parserOptions: {
   ecmaVersion: 6,
    sourceType: 'module',
    ecmaFeatures: {
      jsx: true
    },
    },
  env: {
    browser: true,
    node: true,
    es6: true,
    jest: true,
  },
  ignorePatterns: ['node_modules', 'build', 'dist', 'public'],
  settings: {
    jest: {"version": 27}
  }
};

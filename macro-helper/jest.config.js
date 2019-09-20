const jestConfig = {
  verbose: true,
  bail: true,
  cacheDirectory: "./src/jest-cache",
  collectCoverage: false,
  globals: {
    "__DEV__": true
  },
  moduleNameMapper: {
    "^.+\\.scss$": "identity-obj-proxy"
  },
  setupFiles: ['./jest.setup.js'],
};

module.exports = jestConfig;


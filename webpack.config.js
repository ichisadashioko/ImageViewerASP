var webpack = require('webpack')
var path = require('path')

module.exports = {
    mode: 'development',

    entry: {
        index: './ImageViewerASP/Scripts/src/Index.tsx',
        reader: './ImageViewerASP/Scripts/src/Reader.tsx',
        app: './ImageViewerASP/Scripts/src/App.tsx',
    },
    output: {
        filename: '[name].js',
        path: __dirname + '/ImageViewerASP/Scripts/dist'
    },

    // Enable sourcemaps for debugging webpack's output.
    devtool: 'source-map',

    resolve: {
        // Add '.ts' and '.tsx' as resolvable extensions.
        extensions: ['.ts', '.tsx']
    },

    module: {
        rules: [
            {
                test: /\.ts(x?)$/,
                exclude: /node_modules/,
                use: [
                    {
                        loader: 'ts-loader'
                    },
                    {
                        loader: 'babel-loader',
                    }
                ]
            },
            // All output '.js' files will have any sourcemaps re-processed by 'source-map-loader'.
            {
                enforce: 'pre',
                test: /\.js$/,
                exclude: /node_modules/,
                loader: 'source-map-loader',
            }
        ]
    },

    // When importing a module whose path matches one of the following, just
    // assume a corresponding global variable exists and use that instead.
    // This is important because it allows us to avoid bundling all of our
    // dependencies, which allows browsers to cache those libraries between builds.
    // externals: {
    //     'react': 'React',
    //     'react-dom': 'ReactDOM'
    // }
};
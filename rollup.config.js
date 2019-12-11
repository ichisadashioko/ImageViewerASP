import typescript from 'rollup-plugin-typescript';
import resolve from 'rollup-plugin-node-resolve';
import commonjs from 'rollup-plugin-commonjs';
import replace from 'rollup-plugin-replace';

const extensions = [
    ".js", ".jsx", ".ts", ".tsx"
];

export default {
    input: './ImageViewerASP/Scripts/src/Index.tsx',
    output: [
        {
            name: 'index',
            file: './ImageViewerASP/Scripts/dist/index.umd.js',
            format: 'umd',
            globals: {
                'react': 'React',
                'react-dom': 'ReactDOM',
            },
        },
    ],
    external: [
        'react',
        'react-dom',
    ],
    plugins: [
        resolve({ extensions }),
        typescript(),
        commonjs({
            namedExports: {
                // 'react': ['Component', 'createElement', 'Children', 'cloneElement', 'isValidElement'],
                // 'react-dom': ['render'],
                // 'deepmerge': ['deepmerge'],
                'react-is': ['ForwardRef'],
                'prop-types': ['elementType'],
            }
        }),
        replace({
            'process.env.NODE_ENV': JSON.stringify('development'),
        }),
    ]
}
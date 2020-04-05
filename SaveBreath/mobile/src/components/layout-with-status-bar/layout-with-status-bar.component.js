import * as React from 'react';
import { StatusBar } from 'react-native';
import { Layout } from '@ui-kitten/components';

const LayoutWithStatusBar = ({ layoutStyle, barStyle, children }) => (
  <Layout style={layoutStyle}>
    <StatusBar transculent barStyle={barStyle} />
    {children}
  </Layout>
);

export default LayoutWithStatusBar;

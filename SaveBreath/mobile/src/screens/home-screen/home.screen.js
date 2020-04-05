import * as React from 'react';
import { StyleSheet } from 'react-native';
import { Text } from '@ui-kitten/components';

import LayoutWithStatusBar from '../../components/layout-with-status-bar';

const HomeScreen = () => (
  <LayoutWithStatusBar layoutStyle={styles.layout} barStyle="light-content">
    <Text category="h1">HOME</Text>
  </LayoutWithStatusBar>
);

const styles = StyleSheet.create({
  layout: {
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center',
  },
});

export default HomeScreen;

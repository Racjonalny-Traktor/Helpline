import * as React from 'react';
import { StatusBar, StyleSheet } from 'react-native';
import { Layout, Text } from '@ui-kitten/components';

const HomeScreen = () => (
  <React.Fragment>
    <Layout style={styles.layout}>
      <StatusBar transculent barStyle="light-content" />
      <Text category="h1">HOME</Text>
    </Layout>
  </React.Fragment>
);

const styles = StyleSheet.create({
  layout: {
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center',
  },
});

export default HomeScreen;

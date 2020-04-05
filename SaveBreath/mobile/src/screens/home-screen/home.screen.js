import * as React from 'react';
import { StyleSheet } from 'react-native';
import { Text, Button } from '@ui-kitten/components';

import LayoutWithStatusBar from '../../components/layout-with-status-bar';

const HomeScreen = ({ navigation }) => (
  <LayoutWithStatusBar layoutStyle={styles.layout} barStyle="light-content">
    <Text category="h1">HOME</Text>
    <Button
      status="info"
      appearance="filled"
      style={styles.button}
      onPress={() => navigation.navigate('RecordScreen')}>
      Navigate to Record Page
    </Button>
  </LayoutWithStatusBar>
);

const styles = StyleSheet.create({
  layout: {
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center',
  },
  button: {
    marginTop: 8,
  },
});

export default HomeScreen;

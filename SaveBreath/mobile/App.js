/**
 * Sample React Native App
 * https://github.com/facebook/react-native
 *
 * @format
 * @flow strict-local
 */

import 'react-native-gesture-handler';
import React from 'react';
import { NavigationContainer } from '@react-navigation/native';
import { ApplicationProvider, Layout, Text } from '@ui-kitten/components';
import { mapping, dark as darkTheme } from '@eva-design/eva';

const HomeScreen = () => (
  <Layout style={{ flex: 1, justifyContent: 'center', alignItems: 'center' }}>
    <Text category="h1">HOME</Text>
  </Layout>
);

const App = () => (
  <ApplicationProvider mapping={mapping} theme={darkTheme}>
    <NavigationContainer>
      <HomeScreen />
    </NavigationContainer>
  </ApplicationProvider>
);

export default App;

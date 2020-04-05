/**
 * Sample React Native App
 * https://github.com/facebook/react-native
 *
 * @format
 * @flow strict-local
 */

import * as React from 'react';
import 'react-native-gesture-handler';
import { NavigationContainer } from '@react-navigation/native';
import { ApplicationProvider } from '@ui-kitten/components';
import { mapping, dark as darkTheme } from '@eva-design/eva';

import NavigationProvider from './navigation-provider';

const App = () => (
  <ApplicationProvider mapping={mapping} theme={darkTheme}>
    <NavigationContainer>
      <NavigationProvider />
    </NavigationContainer>
  </ApplicationProvider>
);

export default App;

import * as React from 'react';
import { createStackNavigator } from '@react-navigation/stack';

import HomeScreen from './screens/home-screen';

const { Navigator, Screen } = new createStackNavigator();

const NavigationProvider = () => (
  <Navigator>
    <Screen
      name="Home"
      component={HomeScreen}
      options={{
        headerStatusBarHeight: 0,
        headerStyle: {
          backgroundColor: '#1e263c',
        },
        headerTintColor: '#1e263c',
      }}
    />
  </Navigator>
);

export default NavigationProvider;

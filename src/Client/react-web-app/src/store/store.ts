import { configureStore } from '@reduxjs/toolkit'
import notificationReducer from './slices/notificationSlice'
import predictionsReducer from './slices/predictionsSlice'
import userReducer from './slices/userProfileSlice'

export const store = configureStore({
	reducer: {
		predictions: predictionsReducer,
		notification: notificationReducer,
		user: userReducer,
	},
})

export type RootState = ReturnType<typeof store.getState>
export type AppDispatch = typeof store.dispatch

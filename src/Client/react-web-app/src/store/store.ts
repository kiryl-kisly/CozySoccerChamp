import { configureStore } from '@reduxjs/toolkit'
import notificationReducer from './slices/notificationSlice'
import predictionsReducer from './slices/predictionsSlice'

export const store = configureStore({
	reducer: {
		predictions: predictionsReducer,
		notification: notificationReducer,
	},
})

export type RootState = ReturnType<typeof store.getState>
export type AppDispatch = typeof store.dispatch

import { configureStore } from '@reduxjs/toolkit'
import predictionsReducer from './slices/predictionsSlice'
import userProfileReducer from './slices/userProfileSlice'

export const store = configureStore({
	reducer: {
		userProfile: userProfileReducer,
		predictions: predictionsReducer,
	},
})

export type RootState = ReturnType<typeof store.getState>
export type AppDispatch = typeof store.dispatch

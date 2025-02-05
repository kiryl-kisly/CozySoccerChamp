// src/store/store.ts
import { configureStore } from '@reduxjs/toolkit'
import userProfileReducer from './userProfileSlice'

export const store = configureStore({
	reducer: {
		userProfile: userProfileReducer
	}
})

// Объявляем типы для состояния и Dispatch
export type RootState = ReturnType<typeof store.getState>
export type AppDispatch = typeof store.dispatch

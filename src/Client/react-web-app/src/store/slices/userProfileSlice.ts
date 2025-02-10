import { createSlice, PayloadAction } from '@reduxjs/toolkit'

export interface UserProfileState {
	isEnabledNotification: boolean
}

const initialState: UserProfileState = {
	isEnabledNotification: true,
}

const userProfileSlice = createSlice({
	name: 'userProfile',
	initialState,
	reducers: {
		setEnabledNotification(state, action: PayloadAction<boolean>) {
			state.isEnabledNotification = action.payload
		},
		setInitialUserProfile(state, action: PayloadAction<UserProfileState>) {
			state.isEnabledNotification = action.payload.isEnabledNotification
		},
	},
})

export const { setEnabledNotification, setInitialUserProfile } = userProfileSlice.actions
export default userProfileSlice.reducer
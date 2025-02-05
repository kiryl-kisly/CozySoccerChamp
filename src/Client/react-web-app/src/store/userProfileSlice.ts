import { createSlice, PayloadAction } from '@reduxjs/toolkit'

interface UserProfile {
	isEnabledNotification: boolean
}

interface UserProfileState {
	userProfile: UserProfile | null
}

const initialState: UserProfileState = {
	userProfile: null
}

const userProfileSlice = createSlice({
	name: 'userProfile',
	initialState,
	reducers: {
		setUserProfile(state, action: PayloadAction<UserProfile>) {
			state.userProfile = action.payload
		},
		toggleNotifications(state) {
			if (state.userProfile) {
				state.userProfile.isEnabledNotification = !state.userProfile.isEnabledNotification
			}
		}
	}
})

export const { setUserProfile, toggleNotifications } = userProfileSlice.actions

export default userProfileSlice.reducer

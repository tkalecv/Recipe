using Firebase.Auth;

namespace Recipe.Auth
{
    public interface IFirebaseClient
    {
        FirebaseAdmin.Auth.FirebaseAuth Admin { get; set; }
        FirebaseAuthProvider AuthProvider { get; set; }
    }
}
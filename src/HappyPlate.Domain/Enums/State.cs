using HappyPlate.Domain.Primatives;

namespace HappyPlate.Domain.Enums;

public sealed class State : Enumeration<State>
{
    public static readonly State AL = new(1, "AL", "Alabama");
    public static readonly State AK = new(2, "AK", "Alaska");
    public static readonly State AZ = new(3, "AZ", "Arizona");
    public static readonly State AR = new(4, "AR", "Arkansas");
    public static readonly State CA = new(5, "CA", "California");
    public static readonly State CO = new(6, "CO", "Colorado");
    public static readonly State CT = new(7, "CT", "Connecticut");
    public static readonly State DC = new(8, "DC", "District of Columbia");
    public static readonly State DE = new(9, "DE", "Delaware");
    public static readonly State FL = new(10, "FL", "Florida");
    public static readonly State GA = new(11, "GA", "Georgia");
    public static readonly State HI = new(12, "HI", "Hawaii");
    public static readonly State ID = new(13, "ID", "Idaho");
    public static readonly State IL = new(14, "IL", "Illinois");
    public static readonly State IN = new(15, "IN", "Indiana");
    public static readonly State IA = new(16, "IA", "Iowa");
    public static readonly State KS = new(17, "KS", "Kansas");
    public static readonly State KY = new(18, "KY", "Kentucky");
    public static readonly State LA = new(19, "LA", "Louisiana");
    public static readonly State ME = new(20, "ME", "Maine");
    public static readonly State MD = new(21, "MD", "Maryland");
    public static readonly State MA = new(22, "MA", "Massachusetts");
    public static readonly State MI = new(23, "MI", "Michigan");
    public static readonly State MN = new(24, "MN", "Minnesota");
    public static readonly State MS = new(25, "MS", "Mississippi");
    public static readonly State MO = new(26, "MO", "Missouri");
    public static readonly State MT = new(27, "MT", "Montana");
    public static readonly State NE = new(28, "NE", "Nebraska");
    public static readonly State NV = new(29, "NV", "Nevada");
    public static readonly State NH = new(30, "NH", "New Hampshire");
    public static readonly State NJ = new(31, "NJ", "New Jersey");
    public static readonly State NM = new(32, "NM", "New Mexico");
    public static readonly State NY = new(33, "NY", "New York");
    public static readonly State NC = new(34, "NC", "North Carolina");
    public static readonly State ND = new(35, "ND", "North Dakota");
    public static readonly State OH = new(36, "OH", "Ohio");
    public static readonly State OK = new(37, "OK", "Oklahoma");
    public static readonly State OR = new(38, "OR", "Oregon");
    public static readonly State PA = new(39, "PA", "Pennsylvania");
    public static readonly State RI = new(40, "RI", "Rhode Island");
    public static readonly State SC = new(41, "SC", "South Carolina");
    public static readonly State SD = new(42, "SD", "South Dakota");
    public static readonly State TN = new(43, "TN", "Tennessee");
    public static readonly State TX = new(44, "TX", "Texas");
    public static readonly State UT = new(45, "UT", "Utah");
    public static readonly State VT = new(46, "VT", "Vermont");
    public static readonly State VA = new(47, "VA", "Virginia");
    public static readonly State WA = new(48, "WA", "Washington");
    public static readonly State WV = new(49, "WV", "West Virginia");
    public static readonly State WI = new(50, "WI", "Wisconsin");
    public static readonly State WY = new(51, "WY", "Wyoming");

    State(int id, string name, string description)
        : base(id, name, description)
    {
    }
}